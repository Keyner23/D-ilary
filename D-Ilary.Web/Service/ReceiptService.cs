using System.Reflection.Metadata;
using D_Ilary.Web.Data.Entities;
using D_Ilary.Web.Interfaces.IServices;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;

namespace D_Ilary.Web.Service;

public class ReceiptService: IReceiptService
{
    private readonly IWebHostEnvironment _env;

        public ReceiptService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> GenerateReceiptAsync(Sale sale)
        {
            var receiptsPath = Path.Combine(_env.WebRootPath, "recibos");
            if (!Directory.Exists(receiptsPath))
                Directory.CreateDirectory(receiptsPath);

            var fileName = $"recibo_{sale.Id}.pdf";
            var filePath = Path.Combine(receiptsPath, fileName);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Row(row =>
                    {
                        row.RelativeColumn().Stack(stack =>
                        {
                            stack.Item().Text("D'ILARY - Recibo de Venta").FontSize(18).Bold().FontColor(Colors.Orange.Medium);
                            stack.Item().Text($"Fecha: {sale.SaleDate:dd/MM/yyyy}");
                            stack.Item().Text($"Número de venta: {sale.Id.ToString().Substring(0, 8).ToUpper()}");
                        });
                    });

                    page.Content().Column(col =>
                    {
                        col.Item().PaddingBottom(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        col.Item().Text("Detalles de la Venta").Bold().FontSize(14);
                        col.Item().PaddingBottom(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(150);
                                columns.RelativeColumn();
                                columns.ConstantColumn(100);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Tipo").Bold();
                                header.Cell().Text("Descripción").Bold();
                                header.Cell().AlignRight().Text("Precio").Bold();
                            });

                            if (sale.Product != null)
                            {
                                table.Cell().Text("Producto");
                                table.Cell().Text(sale.Equipment.Name);
                                table.Cell().AlignRight().Text($"${sale.Equipment.priceHour:N0}");
                            }

                            if (sale.Equipment != null)
                            {
                                table.Cell().Text("Equipo");
                                table.Cell().Text(sale.Equipment.Name);
                                table.Cell().AlignRight().Text($"${sale.Equipment.priceHour:N0}");
                            }
                        });

                        col.Item().PaddingTop(20).AlignRight().Text($"Subtotal: ${(sale.Total / 1.19m):N0}");
                        col.Item().AlignRight().Text($"IVA (19%): ${(sale.Total - (sale.Total / 1.19m)):N0}");
                        col.Item().AlignRight().Text($"Total: ${sale.Total:N0}").Bold().FontColor(Colors.Orange.Medium);
                    });

                    page.Footer().AlignCenter().Text("Gracias por su compra").FontSize(10).Italic().FontColor(Colors.Grey.Darken1);
                });
            });

            document.GeneratePdf(filePath);
            await Task.CompletedTask;

            return $"/recibos/{fileName}";
        }
}