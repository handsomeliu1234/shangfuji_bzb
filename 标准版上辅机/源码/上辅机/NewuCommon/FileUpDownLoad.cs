using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace NewuCommon
{
    [DataContract]
    public struct FileInfo_self
    {

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public string FilePath { get; set; }

        [OperationContract]
        public override string ToString()
        {
            return FileName + "," + FilePath;
        }
    }


    public class FileUpDownLoad
    {




        public string FolderBrow()
        {
            string filepath = Application.StartupPath;

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = filepath;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                return fbd.SelectedPath;

            }
            else
            {
                return "";
            }
        }

        public FileInfo[] Fileforeach(string path, out string error)
        {
            error = "";
            try
            {
                DirectoryInfo din = new DirectoryInfo(path);
                FileInfo[] filaArr = din.GetFiles();



                return filaArr;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return new FileInfo[0];
            }
        }





        public Byte[] GetContent(string filepath)  //将某路径下的文件 转化为 二进制代码 
        {
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read); //打开文件流
            Byte[] byData = new Byte[fs.Length]; //保存文件的字节数组
            fs.Read(byData, 0, byData.Length); //读取文件流           
            fs.Close();//释放资源
            return byData;
        }




        public string GetFilePath()
        {
            OpenFileDialog openfiledlg = new OpenFileDialog();
            openfiledlg.CheckPathExists = true;

            if (openfiledlg.ShowDialog() == DialogResult.OK)
            {
                return openfiledlg.FileName;
            }
            else
            {
                return "";
            }
        }




        /// <summary>
        /// 搜索文件夹中的文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public void GetAllFiles(string path, ArrayList FileList, out string error)
        {
            error = "";
            try
            {

                DirectoryInfo dir = new DirectoryInfo(path);


                FileInfo[] allFile = dir.GetFiles();
                foreach (FileInfo fi in allFile)
                {
                    FileInfo_self fs = new FileInfo_self();
                    fs.FilePath = fi.FullName;
                    fs.FileName = fi.Name;

                    FileList.Add(fs);

                }

                DirectoryInfo[] allDir = dir.GetDirectories();
                foreach (DirectoryInfo d in allDir)
                {
                    GetAllFiles(d.FullName, FileList, out error);
                }


            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }

        }




        public void GetAllFilesToTree(string path, TreeNode node, TreeView Tree, out string error)
        {
            error = "";
            try
            {

                DirectoryInfo dir = new DirectoryInfo(path);


                FileInfo[] allFile = dir.GetFiles();
                foreach (FileInfo fi in allFile)
                {
                    if (node == null)
                    {
                        TreeNode P = new TreeNode(fi.Name);

                        Tree.Nodes.Add(P);
                    }
                    else
                    {
                        TreeNode P = new TreeNode(fi.Name);

                        node.Nodes.Add(P);
                    }
                }

                DirectoryInfo[] allDir = dir.GetDirectories();
                foreach (DirectoryInfo d in allDir)
                {
                    TreeNode P = new TreeNode(d.FullName);
                    if (node == null)
                    {
                        Tree.Nodes.Add(P);
                    }
                    else
                    {
                        node.Nodes.Add(P);
                    }
                    GetAllFilesToTree(d.FullName, P, Tree, out error);
                }


            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }

        }


        /// <summary>
        /// 文件夹是否存在，如不存在进行创建
        /// </summary>
        /// <param name="path"></param>
        public void isExitFolderAndCreate(string path)
        {
            bool isExit = Directory.Exists(path);

            if (isExit == false)
            {
                Directory.CreateDirectory(path);
            }
        }


        /// <summary>
        /// 是否存在文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool isExitFile(string filePath)
        {
            return File.Exists(filePath);
        }



    }
}

