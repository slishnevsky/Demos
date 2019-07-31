using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly ILogger _logger;
        private readonly IDataAccess _dataAcess;
        public BusinessLogic(ILogger logger, IDataAccess dataAcess)
        {
            _logger = logger;
            _dataAcess = dataAcess;
        }

        public void ProcessData()
        {
            _logger.Log("Starting data processing data");
            Console.WriteLine("Processing the data");
            _dataAcess.LoadData();
            _dataAcess.SaveData("ProcessedInfo");
            _logger.Log("Finished processing data");
        }
    }
}