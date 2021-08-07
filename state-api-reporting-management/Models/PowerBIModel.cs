
namespace LCU.State.API.LowCodeUnit.Reporting.Management.Models
{
    public class PowerBIModel
    {
        public virtual string Username { get; set; }

        public virtual string Password { get; set; }

        public virtual string ApiUrl { get; set; }

        public virtual string AuthorityUrl { get; set; }

        public virtual string ClientID { get; set; }
        
        public virtual string GroupID { get; set; }
        
        public virtual string ResourceUrl { get; set; }
    }
}