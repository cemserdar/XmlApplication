using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;
using XmlApp.Data;
using XmlApp.Extension;
using XmlApp.Models;
using XmlApp.Services.Abstract;

namespace XmlApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext dataContext;
        private readonly ISBIFService sBIFService;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext, ISBIFService sBIFService)
        {
            _logger = logger;
            this.dataContext = dataContext;
            this.sBIFService = sBIFService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {

            try
            {
                var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);

                var xmlStr = reader.ReadToEnd();
                var sbifBilgileri = DeserializeXml(xmlStr);
                CheckXmlValidations(file);
                CheckDocument(file);

                await sBIFService.AddDocumentAsync(sbifBilgileri);


                return Ok("Başarılı");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        public void CheckDocument(IFormFile file)
        {

            var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);

            var xmlStr = reader.ReadToEnd();
            var sbifBilgileri = DeserializeXml(xmlStr);

            var karsıFirma = sbifBilgileri.FaturaBilgileri.Fatura.KarsiFirmaBilgisi.VergiKimlikNo;
            var sbifId = sbifBilgileri.Id;
            var genelBilgiId = sbifBilgileri.GenelBilgiler.Id;
            var faturaId = sbifBilgileri.FaturaBilgileri.Id;
            var malkalemBilgiId = sbifBilgileri.MalKalemBilgileri.Id;


            var existingRecord = dataContext.SBIFBilgileri.FirstOrDefault(se => se.Id == sbifId);
            var existingRecordGenelBilgi = dataContext.GenelBilgiler.FirstOrDefault(s => s.Id == genelBilgiId);
            var existingRecordFaturaId = dataContext.FaturaBilgileri.FirstOrDefault(f => f.Id == faturaId);
            var existingRecordMalkalemId = dataContext.MalKalemBilgileri.FirstOrDefault(mk => mk.Id == malkalemBilgiId);
            var exisitngRecordKarsiFirma = dataContext.KarsiFirmaBilgileri.FirstOrDefault(kfb => kfb.VergiKimlikNo == karsıFirma);


            if (existingRecord == null && existingRecordGenelBilgi == null && existingRecordFaturaId == null && existingRecordMalkalemId == null && exisitngRecordKarsiFirma == null)
            {
                dataContext.Add(sbifBilgileri);
                dataContext.SaveChanges();

            }
            else if (existingRecord != null)
            {
                throw new Exception($"Bu ID'ye sahip bir kayıt zaten var: {sbifId}");
            }
            else if (existingRecordGenelBilgi != null)
            {
                throw new Exception($"Bu ID'ye sahip bir kayıt zaten var: {genelBilgiId}");
            }
            else if (existingRecordFaturaId != null)
            {
                throw new Exception($"Bu ID'ye sahip bir kayıt zaten var: {faturaId}");
            }
            else if (existingRecordMalkalemId != null)
            {
                throw new Exception($"Bu ID'ye sahip bir kayıt zaten var: {malkalemBilgiId}");
            }
            else if (exisitngRecordKarsiFirma != null)
            {
                throw new Exception($"Bu ID'ye sahip bir kayıt zaten var: {karsıFirma}");
            }

        }

        private SBIFBilgileri DeserializeXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SBIFBilgileri));
            using (var reader = new StringReader(xml))
            {
                return (SBIFBilgileri)serializer.Deserialize(reader);
            }
        }
        private void CheckXmlValidations(IFormFile xmlFile)
        {
            if (xmlFile == null || xmlFile.Length == 0)
                throw new Exception("Yüklenmeye çalışılan dosya geçersiz ya da boş, lütfen geçerli bir dosya deneyiniz.");

            if (!xmlFile.IsXml())
                throw new Exception("Sadece xml uzantılı dosyaları yükleyebilirsiniz.");
        }


        public ActionResult OutputExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SBIFBilgileri");
                worksheet.Cell(1, 1).Value = "BelgeNo";
                worksheet.Cell(1, 2).Value = "DepoKullanimBelgeNo";

                int RowCount = 2;
                foreach (var item in dataContext.GenelBilgiler)
                {
                    worksheet.Cell(RowCount, 1).Value = item.BelgeNo;
                    worksheet.Cell(RowCount, 2).Value = item.DepoKullanimBelgeNo;
                    RowCount++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "deneme.xlsx");
                }

            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
