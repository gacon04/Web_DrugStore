using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdGen;
namespace Web_DrugStore.FuncService
{
    

    public static class IdGeneratorService
    {
        private static readonly IdGenerator _idGenerator;

        static IdGeneratorService()
        {
            // Khởi tạo Epoch (thời gian bắt đầu)
            var epoch = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Cấu hình IdGenerator với 41-bit timestamp, 10-bit máy, 12-bit sequence
            var idStructure = new IdStructure(41, 10, 12); // 41-bit thời gian, 10-bit máy, 12-bit số thứ tự
            var options = new IdGeneratorOptions(idStructure, new DefaultTimeSource(epoch));

            // Khởi tạo IdGenerator với Machine ID = 0 (máy này)
            _idGenerator = new IdGenerator(0, options);
        }

        public static long GenerateId()
        {
            return _idGenerator.CreateId(); // Sinh mã ID
        }
    }

}