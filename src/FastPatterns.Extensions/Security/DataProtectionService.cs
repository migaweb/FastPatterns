using FastPatterns.Core.Abstractions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace FastPatterns.Extensions.Security;

/// <summary>
/// Provides methods for encrypting and decrypting data using data protection.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DataProtectionService"/> class.
/// </remarks>
/// <param name="provider">The data protection provider.</param>
/// <param name="options">The security options containing the encryption key.</param>
/// <param name="logger">The logger instance for logging errors and information.</param>
public class DataProtectionService(
    IDataProtectionProvider provider,
    IOptions<SecurityOptions> options,
    ILogger<DataProtectionService> logger) : IDataProtectionService
{
  private readonly IDataProtector _protector = provider.CreateProtector(options.Value.EncryptionKey);
  private readonly ILogger<DataProtectionService> _logger = logger;

  /// <summary>
  /// Encrypts the specified unencrypted data.
  /// </summary>
  /// <param name="unencryptedData">The data to encrypt.</param>
  /// <returns>The encrypted data.</returns>
  public string Encrypt(string unencryptedData)
      => _protector.Protect(unencryptedData);

  /// <summary>
  /// Decrypts the specified encrypted data.
  /// </summary>
  /// <param name="encryptedData">The data to decrypt.</param>
  /// <returns>The decrypted data, or the original encrypted data if decryption fails.</returns>
  public string Decrypt(string encryptedData)
  {
    try
    {
      return _protector.Unprotect(encryptedData);
    }
    catch (CryptographicException ex)
    {
      _logger.LogError(ex, "Decryption failed. Ensure the data was encrypted with the same key.");
      return encryptedData;
    }
  }
}

