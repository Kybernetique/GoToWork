using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class CertificateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название акта")]
        public string Name { get; set; }
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Имя инженера")]
        public string EngineerName { get; set; }
        public int EngineerId { get; set; }
        public Dictionary<int, string> CertificateProducts { get; set; }
    }
}
