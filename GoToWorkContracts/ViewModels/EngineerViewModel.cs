using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class EngineerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя инженера")]
        public string FIO { get; set; }
        [DisplayName("Логин")]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
