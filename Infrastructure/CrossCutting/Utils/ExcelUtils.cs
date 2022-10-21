using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace CrossCutting.Utils
{
    public class ExcelUtils
    {
        public ExcelUtils() { }

        public static byte[] ListToExcel<T>(List<T> lista, List<string> titulos, string nomePlanilha)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();

            // Definir fonte
            HSSFFont tituloFonte = (HSSFFont)workbook.CreateFont();
            tituloFonte.FontHeightInPoints = 12;
            tituloFonte.FontName = "Century Gothic";
            tituloFonte.Color = IndexedColors.DarkBlue.Index;

            // Definir borda
            HSSFCellStyle tituloCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            tituloCellStyle.SetFont(tituloFonte);
            tituloCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
            tituloCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Medium;
            tituloCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Medium;
            tituloCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Medium;
            tituloCellStyle.VerticalAlignment = VerticalAlignment.Center;

            // Definir fonte
            HSSFFont padraoFonte = (HSSFFont)workbook.CreateFont();
            padraoFonte.FontHeightInPoints = 10;
            padraoFonte.FontName = "Century Gothic";
            padraoFonte.Color = IndexedColors.Automatic.Index;

            // Definir borda
            HSSFCellStyle padraoCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            padraoCellStyle.SetFont(padraoFonte);
            padraoCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            padraoCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            padraoCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            padraoCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            padraoCellStyle.VerticalAlignment = VerticalAlignment.Center;
            padraoCellStyle.WrapText = true;

            ISheet sheet = workbook.CreateSheet(nomePlanilha);
            IRow tituloRow = sheet.CreateRow(0);

            // Adicionar titulos na planilha
            int tituloIndex = 0;
            foreach (var titulo in titulos)
            {
                CreateCell(tituloRow, tituloIndex, titulo, tituloCellStyle);
                sheet.AutoSizeColumn(tituloIndex);
                tituloIndex++;
            }

            // Adicionar itens na tabela
            int padraoRowIndex = 1;
            foreach (var item in lista)
            {
                IRow padraoRow = sheet.CreateRow(padraoRowIndex);
                var propriedades = item.GetType().GetProperties();
                padraoRowIndex++;
                int padraoIndex = 0;

                foreach (var propriedade in propriedades)
                {
                    var valor = propriedade.GetValue(item);
                    CreateCell(padraoRow, padraoIndex, valor.ToString(), padraoCellStyle);
                    sheet.AutoSizeColumn(padraoIndex);
                    padraoIndex++;
                }
            }

            var ms = new MemoryStream();
            workbook.Write(ms);
            return ReadAllBytes(ms);
        }

        public static byte[] ReadAllBytes(Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        private static void CreateCell(IRow currentRow, int cellIndex, string valor, HSSFCellStyle style)
        {
            ICell Cell = currentRow.CreateCell(cellIndex);
            Cell.SetCellValue(valor);
            Cell.CellStyle = style;
        }
    }
}
