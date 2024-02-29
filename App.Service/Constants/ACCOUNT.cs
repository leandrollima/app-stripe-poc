namespace App.Service.Constants
{
    public static class ACCOUNT
    {
        public const string NOT_FOUND = "Account not found";
        public const string IS_LOCKED_OUT = "User is locked";
        public const string IS_NOT_ALLOWED = "is_not_allowed";
        public const string REQUIRES_TWO_FACTOR = "requires_two_factor";
        public const string USERNAME_OR_PASSWORD_INCORRECT = "User or password incorrect";
        public const string EMAIL_CONFIRMATION_IS_REQUIRED = "Email confirmation is required";
        public const string UNABLE_TO_LOGIN = "unable_to_login";
        public const string EMAIL_NOT_FOUND = "E-mail not found";
        public const string UNABLE_TO_CONFIRM_EMAIL = "unable_to_confirm_email";
        public const string UNABLE_TO_REFRESH_CODE = "unable_to_refresh_code";
        public const string INVALID_TOKEN_USER_ID = "invalid_token_user_id";
        public const string USER_NOT_FOUND = "User not found";
        public const string UNABLE_TO_RESET_PASSWORD = "unable_to_reset_password";
        public const string ROLE_NOT_FOUND = "Role not found";
        public const string EXISTING_ACCOUNT_NAME = "Account name already exists";
        public const string EXISTING_EMAIL = "This email already exists";
        public const string USER_BANNED_UNTIL = "User banned until {0}";
        public const string TOKEN_NOT_FOUND_RESET_PASSWORD = "Token not found. Redo the password reset request.";

        public const string MAX_CHARACTER_PER_ACCOUNT = "Você atingiu a quantidade máxima permitida de personagens: {0}";

        public static class TYPE
        {
            public const string FREE = "Free";
            public const string PREMIUM = "Premium";
        }
    }

    public static class STATUS
    {
        public const string ON = "On";
        public const string OFF = "Off";
    }
}
