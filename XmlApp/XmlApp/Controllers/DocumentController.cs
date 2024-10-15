using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using XmlApp.Data;
using XmlApp.Domain.Repositories;
using XmlApp.Extension;
using XmlApp.Helpers;
using XmlApp.Models;
using XmlApp.Services.Abstract;
using XmlApp.ViewModels;

namespace XmlApp.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ISBIFService _sbifService;
        private readonly IGenelBilgilerRepository _genelBilgiRepository;

        public DocumentController(DataContext dataContext, ISBIFService sbifService, IGenelBilgilerRepository genelBilgiRepository)
        {
            _dataContext = dataContext;
            this._sbifService = sbifService;
            _genelBilgiRepository = genelBilgiRepository;
        }

        public async Task<IActionResult> Index()
        {
            var list = (await _sbifService.GetAllAsync()).Select(q => q.ToDataTableModel()).ToList();

            var viewModel = new GumrukViewModel()
            {
                Columns = GetPropertyNames<GumrukDataTableModel>(),
                data = list
            };


            return View(viewModel);
        }

        public static List<string> GetPropertyNames<T>()
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Select(prop => prop.Name)
                            .ToList();
        }

        public IActionResult GetGumrukData()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Upload()
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

                if (sbifBilgileri == null)
                    throw new Exception("Dosya okunamadı!");

                CheckXmlValidations(file);
                await CheckDocumentAsync(sbifBilgileri);

                await _sbifService.AddDocumentAsync(sbifBilgileri);


                return Ok("Başarılı");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        public async Task CheckDocumentAsync(SBIFBilgileri sbifBilgileri)
        {
            var sbifId = sbifBilgileri.Id;
            var genelBilgiId = sbifBilgileri.GenelBilgiler.BelgeNo;

            var existingRecord = await _sbifService.GetByIdAsync(sbifId);
            var existingRecordGenelBilgi = (await _genelBilgiRepository.GetAsync(q => q.BelgeNo == sbifBilgileri.GenelBilgiler.BelgeNo)).FirstOrDefault();


            if (existingRecord == null && existingRecordGenelBilgi == null)
            {
                _dataContext.Add(sbifBilgileri);
                _dataContext.SaveChanges();

            }
            else if (existingRecord != null)
            {
                throw new Exception($"Bu ID'ye sahip bir kayıt zaten var: {sbifId}");
            }
            else if (existingRecordGenelBilgi != null)
            {
                throw new Exception($"Bu ID'ye sahip bir kayıt zaten var: {genelBilgiId}");
            }
           

        }

        public async Task CheckDocumentAsync(IFormFile file)
        {

            var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);

            var xmlStr = reader.ReadToEnd();
            var sbifBilgileri = DeserializeXml(xmlStr);

            var karsıFirma = sbifBilgileri.FaturaBilgileri.Fatura.KarsiFirmaBilgisi.VergiKimlikNo;
            var sbifId = sbifBilgileri.Id;
            var genelBilgiId = sbifBilgileri.GenelBilgiler.Id;
            var faturaId = sbifBilgileri.FaturaBilgileri.Id;
            var malkalemBilgiId = sbifBilgileri.MalKalemBilgileri.Id;


            var existingRecord = await _sbifService.GetByIdAsync(sbifId);
            var existingRecordGenelBilgi = _dataContext.GenelBilgiler.FirstOrDefault(s => s.Id == genelBilgiId);
            var existingRecordFaturaId = _dataContext.FaturaBilgileri.FirstOrDefault(f => f.Id == faturaId);
            var existingRecordMalkalemId = _dataContext.MalKalemBilgileri.FirstOrDefault(mk => mk.Id == malkalemBilgiId);
            var exisitngRecordKarsiFirma = _dataContext.KarsiFirmaBilgileri.FirstOrDefault(kfb => kfb.VergiKimlikNo == karsıFirma);


            if (existingRecord == null && existingRecordGenelBilgi == null && existingRecordFaturaId == null && existingRecordMalkalemId == null && exisitngRecordKarsiFirma == null)
            {
                _dataContext.Add(sbifBilgileri);
                _dataContext.SaveChanges();

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
                foreach (var item in _dataContext.GenelBilgiler)
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
    }
}
