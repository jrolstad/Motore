using Motore.Library.Models.Portfolio;

namespace Motore.Library.Models
{
    public class HomeModel
    {
        private PortfolioCalculationRequestInputModel _portfolioCalculationRequestInputModel = null;

        public virtual PortfolioCalculationRequestInputModel PortfolioRequestCalculationInputModel
        {
            get { return _portfolioCalculationRequestInputModel; }
            set { _portfolioCalculationRequestInputModel = value; }
        }
    }
}