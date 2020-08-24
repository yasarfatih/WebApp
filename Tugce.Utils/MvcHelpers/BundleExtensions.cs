using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Tugce.Utils
{
    public static class BundleExtensions
    {
        public static Bundle CustomOrderer(this Bundle bundle)
        {
            bundle.Orderer = new CustomBundleOrderer();
            return bundle;
        }
    }

    public class CustomBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
