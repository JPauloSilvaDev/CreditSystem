﻿using System.Data;
using System.Text;
using ExcelDataReader;
using Platform.Entity.Interfaces;
using Platform.Entity.ServiceSystem;
using static Platform.Entity.Enums.BatchClientRegisterEnums;


namespace Platform.Transactional.Operations.WorkerOperations
{
    public class ProcessClientRegisterOperations
    {
        private readonly IBatchClientRegisterOperations _batchClientRegister;
        private readonly IClientOperations _clientOperations;

        public ProcessClientRegisterOperations(IBatchClientRegisterOperations batchClientRegister, IClientOperations clientOperations)
        {
            _batchClientRegister = batchClientRegister;
            _clientOperations = clientOperations;
        }

        public async Task ProcessRegisterClientBatch()
        {

            try
            {
                List<BatchClientRegister> batchClientRegister = await _batchClientRegister.GetBatchToProcessByStatusAsync(BatchClientRegisterStatus.Created);
                
                if (!batchClientRegister.Any())
                    throw new CSException();

                foreach (BatchClientRegister batch in batchClientRegister)
                {
                    var filePath = batch.FilePath;

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    #region fazendo leitura do arquivo XLSX | XLS
                    using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                    using var reader = ExcelReaderFactory.CreateReader(stream);
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    #endregion
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    var table = result.Tables[0];

                    int sucessRowsCount = 0;

                    foreach (DataRow row in table.Rows)
                    {
                        var client = new Client
                        {
                            PrimaryName = row["Razão Social"]?.ToString(),
                            SecondaryName = row["Nome Fantasia"]?.ToString(),
                            Document = row["Documento"]?.ToString(),
                            PrimaryPhone = row["Telefone 1"]?.ToString(),
                            SecondaryPhone = row["Telefone 2"]?.ToString(),
                            Email = row["Email"]?.ToString(),
                            ZipCode = row["Cep"]?.ToString(),
                            Neighborhood = row["Bairro"]?.ToString(),
                            Street = row["Rua"]?.ToString(),
                            StreetNumber = short.TryParse(row["Numero"]?.ToString(), out var number) ? number : (short)0,
                            City = row["Cidade"]?.ToString(),
                            State = row["Estado"]?.ToString(),
                            Observation = row["Observação"]?.ToString(),
                            CompanyId = batch.CompanyId

                        };

                       await _clientOperations.InsertClientAsync(client);

                        sucessRowsCount++;
                    }

                    long batchId = batch.BatchClientRegisterId;

                    BatchClientRegisterStatus operationStatus = GetOperationStatus(table.Rows.Count, sucessRowsCount);
                    await _batchClientRegister.UpdateBatchStatusAsync(batchId, operationStatus);
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public BatchClientRegisterStatus GetOperationStatus(int totalRows, int successRows)
        {
            if (successRows == 0)
                return BatchClientRegisterStatus.Error;

            else if (successRows >= 1 && successRows < totalRows)
                return BatchClientRegisterStatus.PartiallyProcessed;

            else
                return BatchClientRegisterStatus.Processed;
        }


    }

}