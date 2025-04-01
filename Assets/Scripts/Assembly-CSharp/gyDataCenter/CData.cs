using System.Collections.Generic;

namespace gyDataCenter
{
	public class CData
	{
		protected Dictionary<int, CDataBase> m_FieldData;

		public CData()
		{
			m_FieldData = new Dictionary<int, CDataBase>();
		}

		public void SetData(int nEnum, CDataBase data)
		{
			if (!m_FieldData.ContainsKey(nEnum))
			{
				m_FieldData[nEnum] = data;
			}
		}

		public int GetDataCount(int nEnum)
		{
			if (!m_FieldData.ContainsKey(nEnum))
			{
				return 0;
			}
			return m_FieldData[nEnum].Count;
		}

		public int GetFieldInt(int nEnum)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != 0)
			{
				return -1;
			}
			return ((CDataBaseInt)m_FieldData[nEnum]).value;
		}

		public float GetFieldFloat(int nEnum)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != kDataType.FLOAT)
			{
				return -1f;
			}
			return ((CDataBaseFloat)m_FieldData[nEnum]).value;
		}

		public bool GetFieldBool(int nEnum)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != kDataType.BOOL)
			{
				return false;
			}
			return ((CDataBaseBool)m_FieldData[nEnum]).value;
		}

		public string GetFieldStr(int nEnum)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != kDataType.STRING)
			{
				return string.Empty;
			}
			return ((CDataBaseString)m_FieldData[nEnum]).value;
		}

		public int GetFieldInt(int nEnum, int nIndex)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != kDataType.ARR_INT)
			{
				return -1;
			}
			return ((CDataBaseArrInt)m_FieldData[nEnum]).GetValue(nIndex);
		}

		public float GetFieldFloat(int nEnum, int nIndex)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != kDataType.ARR_FLOAT)
			{
				return -1f;
			}
			return ((CDataBaseArrFloat)m_FieldData[nEnum]).GetValue(nIndex);
		}

		public bool GetFieldBool(int nEnum, int nIndex)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != kDataType.ARR_BOOL)
			{
				return false;
			}
			return ((CDataBaseArrBool)m_FieldData[nEnum]).GetValue(nIndex);
		}

		public string GetFieldStr(int nEnum, int nIndex)
		{
			if (!m_FieldData.ContainsKey(nEnum) || m_FieldData[nEnum].Type != kDataType.ARR_STRING)
			{
				return string.Empty;
			}
			return ((CDataBaseArrString)m_FieldData[nEnum]).GetValue(nIndex);
		}
	}
}
