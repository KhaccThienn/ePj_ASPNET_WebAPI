using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_WebAPI.Models.Enums
{
    public enum ApplicantStatus
    {
        NOT_IN_PROCESS,
        IN_PROCESS,
        HIRED,
        BANNED
    }
}
