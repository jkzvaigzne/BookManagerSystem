namespace BookManagerSystem.Web.Helpers.Validations
{
    public static class ValidationMessages
    {
        public const string NameExistsValidationMessage = "Book title already exists.";
        public const string NameRequiredValidationMessage = "Book title is required.";
        public const string NameLengthValidationMessage = "Book title must be between 4 and 150 characters long.";
        public const string AuthorRequiredValidationMessage = "Author name is required.";
        public const string AuthorLengthValidationMessage = "Author name must be between 4 and 150 characters long.";
    }
}
