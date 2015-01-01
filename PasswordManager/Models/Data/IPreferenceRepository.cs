using System;

namespace PasswordManager.Models.Data
{
    public interface IPreferenceRepository : IDisposable
    {
        int CategoryId { get; set; }
        int DirectorId { get; set; }
        int ProvinceId { get; set; }
    }
}
