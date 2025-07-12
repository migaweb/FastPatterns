namespace FastPatterns.Extensions.Security;
/// <summary>  
/// Represents the security options for the application.  
/// </summary>  
public class SecurityOptions
{
  /// <summary>  
  /// The name of the security configuration section.  
  /// </summary>  
  public const string Security = "FastPatterns:Extensions:Security";

  /// <summary>  
  /// Gets or sets the encryption key used for securing data.  
  /// </summary>  
  public string EncryptionKey { get; set; } = "default-key";
}
