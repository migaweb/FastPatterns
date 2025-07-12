namespace FastPatterns.Core.Abstractions;
/// <summary>
/// Provides methods for encrypting and decrypting data.
/// </summary>
public interface IDataProtectionService
{
  /// <summary>
  /// Decrypts the specified encrypted data.
  /// </summary>
  /// <param name="encryptedData">The data to decrypt.</param>
  /// <returns>The decrypted data as a string.</returns>
  string Decrypt(string encryptedData);

  /// <summary>
  /// Encrypts the specified unencrypted data.
  /// </summary>
  /// <param name="unencryptedData">The data to encrypt.</param>
  /// <returns>The encrypted data as a string.</returns>
  string Encrypt(string unencryptedData);
}
