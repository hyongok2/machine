#pragma once

namespace LIBIY {
	struct SThreshold {
	public:
		SThreshold()
		{
			nTh1 = 0;
			nTh2 = 0;
			bTh1 = 0;
			bTh2 = 0;
			wTh1 = 0;
			wTh2 = 0;
		}
		short nTh1;
		short nTh2;
		short bTh1;
		short bTh2;
		short wTh1;
		short wTh2;


	};
	struct SInspectParam : SThreshold
	{
	public:
		SInspectParam()
		{
			maxDefect = 100000;
			xOffset = 0.f;
			yOffset = 0.f;
		}
		unsigned int maxDefect;
		float xOffset;
		float yOffset;
	};
	struct SZoneParam {
		SZoneParam() {};
		SZoneParam(int* MatchTable, short *ZoneIdTable, short *ZoneThTable, short *ZoneAddTable, short *ZonePitchIDTable, short *ZoneClassifyTable, \
			__int64 yZoneByte, __int64 ZoneTableWidth, int dxPattern, int dyPattern)
		{
			this->pMatchTable = MatchTable;
			this->ZoneIdAddr = ZoneIdTable;
			this->ZoneTh1Addr = ZoneThTable;
			this->ZoneTh2Addr = ZoneAddTable;
			this->ZoneCMPAddr = ZonePitchIDTable;
			this->ZoneClassifyAddr = ZoneClassifyTable;
			this->yZoneByte = yZoneByte;
			this->ZoneTableWidth = ZoneTableWidth;
			this->dxPattern = dxPattern;
			this->dyPattern = dyPattern;
		}
		int   *pMatchTable;
		short *ZoneIdAddr;
		short *ZoneTh1Addr;
		short *ZoneTh2Addr;
		short *ZoneCMPAddr;
		short *ZoneClassifyAddr;
		__int64   yZoneByte;
		__int64   ZoneTableWidth;
		int   dxPattern;
		int   dyPattern;

		arrInt	yMatchTable;
		arrInt2 MatchTable;

		int yMatchIdx;
		int xMatchIdx;
		int eyPtn;
		int exPtn;

		int inspTop;
		int inspLeft;
		int inspBottom;
		int InspRight;

		void SetInspRect(int inspTop, int InspLeft, int inspBottom, int InspRight)
		{
			this->inspTop = inspTop;
			this->inspLeft = inspLeft;
			this->inspBottom = inspBottom;
			this->InspRight = InspRight;
		}
		inline void SetZoneParam()
		{
			int idx = 0;
			while (1)
			{
				int yMatch = pMatchTable[yZoneByte*idx + 1];
				idx++;
				yMatchTable.push_back(yMatch);
				if (yMatch + dyPattern > inspBottom)
				{
					break;
				}
			}
			yMatchIdx = 0;
			eyPtn = yMatchTable[0] + dyPattern;
			SetMatchTable();
		}
		inline void SetMatchTable()
		{
			int idx = 0;
			MatchTable.clear();
			while (1)
			{
				int xMatch = pMatchTable[yMatchIdx*yZoneByte + idx + 0];
				int yMatch = pMatchTable[yMatchIdx*yZoneByte + idx + 1];
				idx += 2;
				_int2 Match;
				Match.x = xMatch;
				Match.y = yMatch;
				MatchTable.push_back(Match);
				if (xMatch + dxPattern > InspRight)
				{
					break;
				}
				xMatchIdx = 0;
				exPtn = MatchTable[0].x + dxPattern;
			}
		}
		inline void SetPtn()
		{

			eyPtn = yMatchTable[yMatchIdx];
			SetMatchTable();

		}

	};
	struct SFreeformParam
	{
		SFreeformParam()
		{
			pMaskBufferOrigin = NULL;
			pMaskBuffer = NULL;
			nWidthStepMask = 0;
			nMOffset = 0;
			nMOffsetSecond = 0;
			nLabel = 0;
		}
		unsigned char *pMaskBufferOrigin;
		unsigned char *pMaskBuffer;
		int nWidthStepMask;
		__int64 nMOffset;
		__int64 nMOffsetSecond;
		int nLabel;//char
	};

	struct SPeriodInspectionParam : SInspectParam
	{
		SPeriodInspectionParam()
		{
			fPitch = 0.0;
			fPitch2 = 0.0;
			fxPitch = 0.0;
			fxPitch2 = 0.0;
			xOffset = 0;
			yOffset = 0;
			supress = 0;
			pPitchArray = NULL;
			bUse2x2Filter = 0;
		}
		float fPitch;
		float fPitch2;
		float fxPitch;
		float fxPitch2;

		int xOffset;
		int yOffset;
		int supress;
		SThreshold Thresholds;
		SZoneParam ZoneParam;
		SFreeformParam FreeformParam;
		int *pPitchArray;
		bool bUse2x2Filter;
	};

	struct SReferenceInspectionParam : SInspectParam
	{
	public:
		SReferenceInspectionParam()
		{
			scale = 1.f;
			scale2 = 1.f;
			rad = 0.f;
			rotationCenterX = 0.f;
			rotationCenterY = 0.f;
			zoneIndex = 255;
			temp1 = 0;
			temp2 = 0;
		}
		float scale;
		float scale2;
		float rad;
		float rotationCenterX;
		float rotationCenterY;
		int zoneIndex;
		int temp1;
		int temp2;
	};
	struct SDefectResult
	{
		int srcGray;
		int dstGray;
	};
	struct SDefectZoneResult
	{
		int ZoneID;
		int ZoneTh;
		int ZoneClassID;
	};

	typedef std::vector< SDefectResult> arrDefectResult;
	typedef std::vector< SDefectZoneResult> arrDefectZoneResult;

#ifndef __STL_ARRAY_NOT_SUPPORT__
	struct SInspectionOutput
	{
		SInspectionOutput() {};
		SInspectionOutput(int maxDefect)
		{
			WDefectPoint.resize(maxDefect);
			BDefectPoint.resize(maxDefect);
			WDefectResult.resize(maxDefect);
			BDefectResult.resize(maxDefect);
			WZoneResult.resize(maxDefect);
			BZoneResult.resize(maxDefect);
			nWDefects = 0;
			nBDefects = 0;
		}
		arrDefectResult WDefectResult;
		arrDefectResult BDefectResult;
		arrDefectZoneResult WZoneResult;
		arrDefectZoneResult BZoneResult;
		KDTreeArrayInt2 WDefectPoint;
		KDTreeArrayInt2 BDefectPoint;
		unsigned int nWDefects = 0;
		unsigned int nBDefects = 0;
	};


	struct SInspectionOutputBuffer
	{
		SInspectionOutputBuffer()
		{	};
		SInspectionOutputBuffer(int maxDefect) :InspectOutput(maxDefect)
		{
			WDefectPoint = (_int2*)InspectOutput.WDefectPoint.data();
			BDefectPoint = (_int2*)InspectOutput.BDefectPoint.data();
			WDefectResult = InspectOutput.WDefectResult.data();
			BDefectResult = InspectOutput.BDefectResult.data();
			WZoneResult = InspectOutput.WZoneResult.data();
			BZoneResult = InspectOutput.BZoneResult.data();
			nWDefects = 0;
			nBDefects = 0;
		}
		SInspectionOutputBuffer(SInspectionOutput &InspectOutput)
		{
			WDefectPoint = (_int2*)InspectOutput.WDefectPoint.data();
			BDefectPoint = (_int2*)InspectOutput.BDefectPoint.data();
			WDefectResult = InspectOutput.WDefectResult.data();
			BDefectResult = InspectOutput.BDefectResult.data();
			WZoneResult = InspectOutput.WZoneResult.data();
			BZoneResult = InspectOutput.BZoneResult.data();
			nWDefects = 0;
			nBDefects = 0;
		}

		SInspectionOutput InspectOutput;
		SDefectResult* WDefectResult;
		SDefectResult* BDefectResult;
		SDefectZoneResult *WZoneResult;
		SDefectZoneResult *BZoneResult;
		_int2* WDefectPoint;
		_int2* BDefectPoint;
		unsigned int nWDefects;
		unsigned int nBDefects;
	};
#endif
}