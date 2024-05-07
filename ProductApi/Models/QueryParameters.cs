namespace ProductApi.Models
{
    public class QueryParameters
    {
        private int _maxSize = 20;
        private int _size = 5;
        public int Page { get; set; } = 1;
        public int Size
        {
            get
            {
                return this._size;
            }
            set
            {
                _size = Math.Min(this._maxSize, value);
            }
        }
    }
}
