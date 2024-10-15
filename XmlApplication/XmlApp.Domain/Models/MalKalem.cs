﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XmlApp.Models.Fields
{
    public class MalKalem
    {
        [Key]
        public string MalKalemId { get; set; }
        public int SiraNo { get; set; }
        public int GumrukSiraNo { get; set; }
        public string? ReferansFormTipi { get; set; }
        public string? ReferansFormNo { get; set; }
        public int ReferansFormYil { get; set; }
        public int ReferansFormSiraNo { get; set; }
        public string gtip { get; set; }
        public string Cins { get; set; }
        public string Mensei { get; set; }
        public string GeldigiGidecegiUlke { get; set; }
        public decimal BirinciMiktar { get; set; }
        public string BirinciBirim { get; set; }
        public string KapTuru { get; set; }
        public int KapAdedi { get; set; }
        public decimal MalBedeli { get; set; }
        public string MalBedeliParaBirimi { get; set; }
        public decimal NavlunBedeli { get; set; }
        public string NavlunBedeliParaBirimi { get; set; }
        public decimal SigortaBedeli { get; set; }
        public string SigortaBedeliParaBirimi { get; set; }
        public decimal BrutAgirlik { get; set; }
        public string MalKalemTeslimSekli { get; set; }
        [NotMapped]
        public MalKalemBilgileri MalKalemBilgileri { get; set; }
    }

}
