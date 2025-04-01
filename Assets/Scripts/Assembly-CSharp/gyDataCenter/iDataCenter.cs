using System.Collections.Generic;
using System.Xml;

namespace gyDataCenter
{
	public class iDataCenter
	{
		protected struct Config
		{
			public int nEnum;

			public string sFieldName;

			public int nDataType;

			public bool isKey;

			public Config(int nenum, string fieldname, kDataType datatype, bool isKey = false)
			{
				nEnum = nenum;
				sFieldName = fieldname;
				nDataType = (int)datatype;
				this.isKey = isKey;
			}
		}

		protected Dictionary<int, FieldInfo> m_dictField;

		protected Dictionary<int, CData> m_dictData;

		protected string m_sXPath;

		public iDataCenter()
		{
			m_dictField = new Dictionary<int, FieldInfo>();
			m_dictData = new Dictionary<int, CData>();
		}

		public virtual void Initialize()
		{
		}

		protected void Initialize(Config[] arrConfig, string xpath)
		{
			if (arrConfig != null)
			{
				for (int i = 0; i < arrConfig.Length; i++)
				{
					m_dictField.Add(arrConfig[i].nEnum, new FieldInfo(arrConfig[i].sFieldName, (kDataType)arrConfig[i].nDataType, arrConfig[i].isKey));
				}
				m_sXPath = xpath;
			}
		}

		public virtual bool Load()
		{
			return true;
		}

		protected bool Load(string content)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(content);
			return XMLManager.Load(xmlDocument, m_sXPath, m_dictField, m_dictData);
		}

		public CData GetData(int nKey)
		{
			if (!m_dictData.ContainsKey(nKey))
			{
				return null;
			}
			return m_dictData[nKey];
		}
	}
}
