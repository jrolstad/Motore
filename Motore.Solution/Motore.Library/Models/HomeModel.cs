using Motore.Library.Models.Portfolio;

namespace Motore.Library.Models
{
    public class HomeModel
    {
        private PortfolioCalculationRequestInputModel _portfolioCalculationRequestInputModel = null;

        public virtual PortfolioCalculationRequestInputModel PortfolioCalculationRequestInputModel
        {
            get
            {
                if (_portfolioCalculationRequestInputModel == null)
                {
                    _portfolioCalculationRequestInputModel = new PortfolioCalculationRequestInputModel();
                }
                return _portfolioCalculationRequestInputModel;
            }
            set { _portfolioCalculationRequestInputModel = value; }
        }
    }
}