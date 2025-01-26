using TrelloCopy.Common.Data.Enums;

namespace TrelloCopy.Models
{
    public class UserFeature : BaseModel
    { 
        public int UserID { get; set; }
        public User user { get; set; }
        public Feature Feature { get; set; }
    }
}
