namespace BlogWebApplication.Core.Models.ArticleModels
{
    public class ArticleParameters
    {
        public virtual int CurrentPage { get; set; } = 1; // mevcut sayfa
        public virtual int PageSize { get; set; } = 3; // her sayfa için listelenecek entity adeti
        public virtual int TotalCount { get; set; } // toplam entity sayısı
        public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize)); // Toplam entity sayısını her sayfada listelenecek entity sayısına bölerek toplam sayfa sayısını bulabiliriz.
        public virtual bool ShowPrevious => CurrentPage > 1; // Mevcut sayfa 1 rakamından büyükse kendisinden önceki sayfaya gitme butonu << aktif, değilse pasif.
        public virtual bool ShowNext => CurrentPage < TotalPages; // mevcut sayfa Toplam Sayfa sayısından küçükse Sonraki >> butonu aktif, değilse pasif
        public virtual bool IsAscending { get; set; } = false; // sıralama
    }
}
