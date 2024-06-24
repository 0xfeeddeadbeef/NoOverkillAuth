namespace NoOverkillAuth;

public static class AuthenticationMethods
{
    public const string MultiFactorAuthentication = "mfa";
    public const string OneTimePassword = "otp";
}

public static class JwtClaimTypes
{
    /// <summary>
    ///   Authentication Methods References.
    ///   JSON array of strings that are identifiers for authentication methods used in the authentication.
    /// </summary>
    public const string AuthenticationMethod = "amr";
}
