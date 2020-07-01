using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SIXTReservationApp.Auth;
using SIXTReservationApp.DesignPattern;
using SIXTReservationApp.Models;
using SIXTReservationBL;
using SIXTReservationBL.CoreBL;
using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Hendlers;
using SIXTReservationBL.Models;
using SIXTReservationBL.Models.Domain;
using SIXTReservationBL.Models.ViewModels;
using SIXTReservationBL.Repositories;

namespace SIXTReservationApp.Controllers
{
    [AppAuthorize]
    public class UploadsController : BaseController
    {
        // test branch 


        private readonly IUnitOfWork unitOfWork;
        IWebHostEnvironment hostEnvironment;

        public UploadsController(IUnitOfWork _UnitOfWork, IWebHostEnvironment _HostEnvironment)
        {
            unitOfWork = _UnitOfWork;
            hostEnvironment = _HostEnvironment;
        }
        [PermissionNotRequired]
        public IActionResult Index()
        {
            return View();
        }
        [PermissionNotRequired]
        [HttpPost]
        public IActionResult UploadReservations(IFormFile fileExcel)
        {
            //NOT COMPLETED 
            try
            {
                if (fileExcel.Length == 0)
                {
                    return Json(new { Ok = false, Message = "File is empty" });
                }
                List<string> ErrorCodes = new List<string>();
                var fileName = Path.GetFileName(fileExcel.FileName);
                string ext = Path.GetExtension(fileExcel.FileName);

                string NewFileName = "Upload_" + DateTime.Now.ToShortDateString().Replace("/", "-") + "-" + DateTime.Now.ToShortTimeString().Replace(":", "-").Replace(" ", "-") + ext;
                 
                var webRoot = hostEnvironment.WebRootPath;
                string path = System.IO.Path.Combine(webRoot, "Uploads");
                Directory.CreateDirectory(path);
                string Fullpath = Path.Combine(path, NewFileName);
                var webPath = Path.Combine("Uploads", NewFileName);
                IWorkbook workbook;

                if (ext == ".xlsx")
                {
                    workbook = new XSSFWorkbook(fileExcel.OpenReadStream());
                }
                else
                {
                    workbook = new HSSFWorkbook(fileExcel.OpenReadStream());
                }
                var NumberOfSheets = workbook.NumberOfSheets;
                ISheet sheet = workbook.GetSheetAt(0); //  first work sheet only 
                IRow headerRow = sheet.GetRow(0);

                var hearderRowCount = headerRow.Cells.Count();

                var lastRowNum = sheet.LastRowNum;


                try
                {
                    using (var stream = new FileStream(Fullpath, FileMode.Create))
                    {
                        fileExcel.CopyTo(stream);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { Ok = false, Message = "Failed to save file to disk" });
                }
                int uploadLogId = SaveNewUploadLog(webPath, lastRowNum, fileName);
                if (uploadLogId > 0)
                {
                    return Json(new { Ok = true, Message = "File uploaded successfully and will be parsed soon" });
                }
                else
                {
                    return Json(new { Ok = false, Message = "Failed To save upload log" });
                }

                //int[] result = SaveUploadfileToDB(sheet, lastRowNum, uploadLogId, ref ErrorCodes);

                //string msg = result[0] + " records saved successfully ";
                //if (result[1] > 0)
                //{
                //    msg += " , and " + result[1] + " records failed";
                //}

                //return Json(new
                //{
                //    Ok = true,
                //    Message = msg,
                //    SavedRecords = result[0],
                //    FialedRecords = result[1],
                //    ErrorCodes = ErrorCodes?.Count > 0 ? JsonConvert.SerializeObject(ErrorCodes) : null,

                //});
            }
            catch (Exception e)
            {
                return Json(new { Ok = false, Message = "Only excel files are accepted" });

            }

        }
        protected int SaveNewUploadLog(string path, int lastRowNum, string uploadedFileName)
        {
            UploadLog uploadLog = new UploadLog()
            {
                FilePath = path,
                CreationTime = DateTime.Now,
                UserId = LoggedUserId,
                NumberOfEntries = lastRowNum,
                UploadStatus = (int)UploadStatusEnum.Uploaded,
                OriginalFileName = uploadedFileName
            };
            unitOfWork.UploadLogBL.Add(uploadLog);
            if (unitOfWork.Complete() > 0)
            {
                return uploadLog.Id;
            }
            else return -1;



        }


        protected void ReadUsingClosedXML(IFormFile fileExcel, string fullPath)
        {

            using (XLWorkbook wb = new XLWorkbook(fileExcel.OpenReadStream()))
            {
                IXLWorksheet worksheet = wb.Worksheet(1);
                wb.SaveAs(fullPath);
                bool FirstRow = true;
                //Range for reading the cells based on the last cell used.  
                string readRange = "1:1";
                var sheetRows = worksheet.RowsUsed().ToArray();
                var rowscount = sheetRows.Length;
                for (int i = 0; i < rowscount; i++)
                {
                    IXLRow row = sheetRows[i];
                    //If Reading the First Row (used) then add them as column name  
                    if (FirstRow)
                    {
                        var rowCells = row.Cells(readRange).ToArray();
                        var resNo = rowCells[0].Value;

                    }


                }

            }
        }
        // comment to push 
     
        public ViewResult UploadsIndex()
        {
            return View();
        }
       
        public PartialViewResult _UploadsList(UploadSC uploadSC)
        {
            try
            {
                var model = SearchUploads(uploadSC);

                return PartialView(model);
            }
            catch (Exception e)
            {
                return PartialView(null);
            }
        }

        private UploadLogVM[] SearchUploads(UploadSC uploadSC)
        {
            var model = unitOfWork.UploadLogBL.GetAllUploadsWithDetails(uploadSC)
                                                                    .Select(u => new UploadLogVM(u))
                                                                    .ToArray();
            return model;

        }
    }
}