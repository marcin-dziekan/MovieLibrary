using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoLib.Models;
using PagedList;
using WMPLib;

namespace VideoLib.Controllers
{
    public class HomeController : Controller
    {
        private List<Movie> _MList;
        public HomeController()
        {
            _MList = GetMovieList();
        }

        [Authorize]
        public ActionResult MovieList(string sort, int? page, string searchString, string currentFilter)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sort;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.DateSortParm = sort == "Date" ? "date_desc" : "Date";
            if (!String.IsNullOrEmpty(searchString))
            {
                _MList = _MList.Where(s => s.FullName.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sort)
            {
                case "name_desc":
                    _MList.OrderByDescending(x => x.FullName);
                    break;
                case "date_desc":
                    _MList.OrderBy(x => x.FullName);
                    break;
                case "Date":
                    _MList.OrderBy(x => x.Date);
                    break;
                default:
                    _MList.OrderBy(x => x.FullName);
                    break;
            }

            int pageSize = 14;
            int pageNumber = (page ?? 1);
            return View(_MList.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public ActionResult Movie(string id, string drive, string name)
        {
            return View(new Movie { Path = id+".mp4", WitchDrive = drive, FullName = name, MovieList = GetMovieList()});
        }


        #region Helpers

        private List<Movie> GetMovieList()
        {
            var movieList = new List<Movie>();
            try
            {
                movieList.AddRange(ScanDisk("D:\\", "res_1"));
                //movieList.AddRange(ScanDisk("H:\\", "res_2"));
                return movieList;
            }
            catch (FileLoadException)
            {
                return movieList;
            }
        }

        // vFolder - virtual folder created on IIS, if you have your movies on different drive like D:\
        // diskPath - path to drive where you have your movies
        private List<Movie> ScanDisk(string diskPath, string vFolder)
        {
            var movieList = new List<Movie>();
            var path2 = Directory.GetFiles(@"" + diskPath + "");
            foreach (var z in path2)
            {
                if (Path.GetExtension(z) == ".mp4")
                {
                    //var player = new WindowsMediaPlayer();
                    //var clip = player.newMedia(z);
                    var item = new Movie
                    {
                        Path = Path.GetFileName(z).ToString(),
                        FullName = Path.GetFileNameWithoutExtension(z),
                        WitchDrive = vFolder,
                        Date = System.IO.File.GetCreationTime(z),
                        //MovieTime = clip.durationString,
                    };
                    movieList.Add(item);
                }
            }
            return movieList;
        }


        public JsonResult SearchMovie (string movieName)
        {
            var selectedMovies = _MList.Where(x => x.FullName.Contains(movieName)).Select(x => x);
            return Json(selectedMovies.ToArray(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    } 
}