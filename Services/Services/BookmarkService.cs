using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        private readonly ICategoryService _categoryService;

        public BookmarkService(ReadLaterDataContext readLaterDataContext, ICategoryService categoryService)
        {
            _ReadLaterDataContext = readLaterDataContext;
            _categoryService = categoryService;
        }
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            var category = _categoryService.GetCategory(bookmark.Category.Name, bookmark.UserId);
            if (category == null)
            {
                category = _categoryService.CreateCategory(bookmark.Category);
            }
            bookmark.CreateDate = DateTime.Now;
            bookmark.CategoryId = category.ID;
            bookmark.Category = category;

            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public Bookmark GetBookmark(int Id)
        {
            return _ReadLaterDataContext.Bookmark.Where(b => b.ID == Id).FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks(string userId)
        {
            return _ReadLaterDataContext.Bookmark.Where(b => b.UserId == userId).ToList();
        }
        public List<Bookmark> GetMostRecentBookmarks(string userId)
        {
            return _ReadLaterDataContext.Bookmark.OrderByDescending(b => b.UserId == userId).Take(5).ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            var category = _categoryService.GetCategory(bookmark.Category.Name, bookmark.UserId);
            if (category == null)
            {
                category = _categoryService.CreateCategory(bookmark.Category);
            }
            bookmark.CreateDate = DateTime.Now;
            bookmark.CategoryId = category.ID;
            bookmark.Category = category;

            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
