using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using ResponseEmergencySystem.Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Reports
{
    public static class PDFSharp
    {
        public static void AddLogo(XGraphics gfx, PdfPage page, string imagePath, int xPosition, int yPosition)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException(String.Format("Could not find image {0}.", imagePath));
            }

            XImage xImage = XImage.FromFile(imagePath);
            gfx.DrawImage(xImage, xPosition, yPosition, xImage.PixelWidth, xImage.PixelWidth);
        }

        public static void PDF()
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string ext = System.IO.Path.GetExtension(ofd.FileName).ToUpper();
                        try
                        {
                                if (ext == ".PDF")
                                {
                                    PdfSharp.Pdf.PdfDocument PDFDoc = PdfSharp.Pdf.IO.PdfReader.Open(ofd.FileName, PdfDocumentOpenMode.Import);
                                    PdfSharp.Pdf.PdfDocument PDFNewDoc = new PdfSharp.Pdf.PdfDocument();
                                    for (int Pg = 0; Pg < PDFDoc.Pages.Count; Pg++)
                                    {
                                        PdfPage pdfPage = PDFNewDoc.AddPage(PDFDoc.Pages[Pg]);
                                        XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                                        XFont font = new XFont("Times New Roman", 20, XFontStyle.Regular);
                                        graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
                                }

                                    PDFNewDoc.Save($"{Path.GetDirectoryName(ofd.FileName)}\\TEST.pdf");
                                }
                                else
                                {
                                    Utils.ShowMessage("The file submitted is not an Image or PDF", title: "Image upload error", type: "Warning");
                                }

                            //PdfDocument pdf = new PdfDocument();
                            //pdf.Info.Title = "My First PDF";
                            //PdfPage pdfPage = pdf.AddPage();
                            //XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                            //XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
                            //graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
                            //string pdfFilename = "firstpage.pdf";
                            //pdf.Save(pdfFilename);
                            //Process.Start(pdfFilename);

                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Utils.ShowMessage(ex.Message, title: "Image upload error", type: "Error");
                        }

                    }

                }

                
            }
            catch (Exception)
            {
                // Handle exception
            }
        }

    }
}
