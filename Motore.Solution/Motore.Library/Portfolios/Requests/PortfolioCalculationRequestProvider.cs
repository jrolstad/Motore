using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motore.Library.Aws;
using Motore.Library.Aws.S3;
using Motore.Library.Aws.SimpleDb;
using Motore.Library.Configuration;
using Motore.Library.Entities;
using Motore.Library.Models.Portfolio;
using Motore.Utils.Assertions;
using Motore.Utils.Dates;
using Motore.Utils.Logging;

namespace Motore.Library.Portfolios.Requests
{
    public class PortfolioCalculationRequestProvider
    {
        private SimpleDbClient _simpleDbClient = null;
        private S3Client _s3Client = null;

        public virtual IEnumerable<PortfolioCalculationRequestViewModel> GetMostRecent100Requests()
        {
            string nextToken = null;
            var requests = this.SimpleDbClient.Get<PortfolioCalculationRequest>(100, ref nextToken);
            return this.Convert(requests);
        }

        public virtual PortfolioCalculationRequestSubmitModel SubmitRequest(PortfolioCalculationRequestInputModel input)
        {
            // save the request id
            Assert.Fail(() => (input != null), "The PortfolioCalculationRequestModel parameter is null");
            Assert.Fail(()=>(!String.IsNullOrWhiteSpace(input.RequestId)), "The RequestId property of the PortfolioCalculationRequestModel is null or whitespace");

            var requestId = this.SaveInitialRequest(input);
            var fileInfo = this.SavePortfolioFile(input);
            this.SaveUserFileRecord(requestId, fileInfo);

            var model = new PortfolioCalculationRequestSubmitModel();
            throw new NotImplementedException();
        }

        public virtual void LogRequestError(string requestId, string error)
        {
            var message = String.Format("Portfolio Calculation Request Error [ID {0}]: {1}", requestId, error);
            Log.LogException(message);
        }

        public virtual string CreateNewRequestId()
        {
            return Guid.NewGuid().ToString();
        }

        #region Protected Methods

        protected internal virtual string SaveUserFileRecord(string requestId, PortfolioFileInfo fileInfo)
        {
            var nowTimestamp = SystemTime.Now().ToTimestamp();

            var file = new UserFile
                                {
                                    ClientFileName = fileInfo.ClientFileName,
                                    CreatedBy = "system",
                                    ModifiedBy = "system",
                                    CreateTimestamp = nowTimestamp,
                                    ModifyTimestamp = nowTimestamp,
                                    FileSystemType = fileInfo.FileSystemType,
                                    Location = fileInfo.Uri,
                                    RequestId = requestId,
                                    Status = UserFileStatus.Pending,
                                    UploadTimestamp = fileInfo.UploadTimestamp,
                                    UserFileType = UserFileType.Portfolio,
                                };

            var info = this.SimpleDbClient.SaveEntity<UserFile>(file);
            return info.PrimaryKey;
        }

        protected internal virtual PortfolioFileInfo SavePortfolioFile(PortfolioCalculationRequestInputModel input)
        {
            try
            {
                string clientFileName = input.ClientFileName;
                var path = this.CalculatePortfolioFileSavePath(input);
                var putInfo = this.S3Client.Save(input.FileStream, this.PortfolioBucketName, path);
                var portfolioFileInfo = this.ConvertToPortfolioFileInfo(putInfo);
                portfolioFileInfo.ClientFileName = clientFileName;
                return portfolioFileInfo;
            }
            catch (Exception exc)
            {
                this.LogRequestError(input.RequestId, exc.ToString());
                throw;
            }
        }

        protected internal virtual PortfolioFileInfo ConvertToPortfolioFileInfo(S3PutInfo putInfo)
        {
            var portfolioFileInfo = new PortfolioFileInfo
                                        {
                                            FileSystemType = FileSystemType.S3,
                                            Uri = putInfo.Uri,
                                            UploadTimestamp = SystemTime.Now().ToTimestamp(),
                                        };

            return portfolioFileInfo;
        }

        protected internal virtual string CalculatePortfolioFileSavePath(PortfolioCalculationRequestInputModel input)
        {
            // how do we determine where to save the file?    
            var folder = this.GetS3PortfolioFolder(input);
            var fileName = this.GetS3PortfolioFileName(input);
            const string fmt = "{0}/{1}";
            var path = String.Format(fmt, folder, fileName);
            return path;
        }

        protected internal virtual string GetS3PortfolioFileName(PortfolioCalculationRequestInputModel input)
        {
            // what do we know about the input file?
            return String.Format("{0}.{1}.port", input.RequestId, input.PortfolioFileType);
        }

        protected internal virtual string GetS3PortfolioFolder(PortfolioCalculationRequestInputModel input)
        {
            var now = SystemTime.Now().ToString("yyyy-MM-dd");
            return now;
        }
    
        protected internal virtual string PortfolioBucketName
        {
            get { return Config.PortfolioBucketName; }
        }

        protected internal virtual IEnumerable<PortfolioCalculationRequestViewModel> Convert(List<PortfolioCalculationRequest> requests)
        {
            IEnumerable<PortfolioCalculationRequestViewModel> models = new List<PortfolioCalculationRequestViewModel>();
            if ((requests != null)
                && (requests.Count > 0))
            {
                models = requests.Select(Convert);
            }
            return models;
        }

        protected internal virtual PortfolioCalculationRequestViewModel Convert(PortfolioCalculationRequest request)
        {
            var model = new PortfolioCalculationRequestViewModel
                            {
                                RequestId = request.RequestId,
                                RequestTimestamp = request.RequestTimestamp,
                                ClientIp = request.ClientIp,
                                CreatedBy = request.CreatedBy,
                                CreateTimestamp = request.CreateTimestamp,
                                ModifiedBy = request.ModifiedBy,
                                ModifyTimestamp = request.ModifyTimestamp,
                                PortfolioFileInfo = request.PortfolioFileInfo,
                                Status = request.Status,
                            };
            return model;
        }

        protected internal virtual PortfolioCalculationRequest Convert(PortfolioCalculationRequestInputModel model)
        {
            var request = new PortfolioCalculationRequest
                              {
                                  ClientIp = model.ClientIp,
                                  CreatedBy = model.CreatedBy,
                                  CreateTimestamp = model.CreateTimestamp,
                                  ModifiedBy = model.ModifiedBy,
                                  ModifyTimestamp = model.ModifyTimestamp,
                                  Origin = model.Origin,
                                  RequestTimestamp = model.RequestTimestamp,
                                  RequestId = model.RequestId,
                                  Status = PortfolioCalculationRequestStatus.Pending,

                              };

            return request;
        }

        protected internal virtual string SaveInitialRequest(PortfolioCalculationRequestInputModel model)
        {
            var request = this.Convert(model);
            this.SimpleDbClient.SaveEntity<PortfolioCalculationRequest>(request);
            return request.RequestId;
        }

        #endregion

        #region Protected Properties

        protected internal virtual SimpleDbClient SimpleDbClient
        {
            get { return _simpleDbClient ?? (_simpleDbClient = AwsClientFactory.CreateSimpleDbClient()); }
            set { _simpleDbClient = value; }
        }


        protected internal virtual S3Client S3Client
        {
            get { return _s3Client ?? (_s3Client = AwsClientFactory.CreateS3Client()); }
            set { _s3Client = value; }
        }

        #endregion

    }
}
