using System.Collections.Generic;

namespace Blogrum.Core.Services
{
    public abstract class BaseResult
    {
        public IList<string> Errors { get; set; }

        public BaseResult()
        {
            this.Errors = new List<string>();
        }

        public bool Success
        {
            get
            {
                return this.Errors.Count == 0;
            }
        }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }
    }
}
