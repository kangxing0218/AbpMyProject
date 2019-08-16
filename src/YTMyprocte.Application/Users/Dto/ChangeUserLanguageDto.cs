using System.ComponentModel.DataAnnotations;

namespace YTMyprocte.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}