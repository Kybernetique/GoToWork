using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class BossViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя начальника")]
        public string FIO { get; set; }
        [DisplayName("Логин")]
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
