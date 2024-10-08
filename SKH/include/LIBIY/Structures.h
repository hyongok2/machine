#pragma once
#ifndef LIBIY_COMMONOBJECT_STRUCTURES
#define LIBIY_COMMONOBJECT_STRUCTURES
#include <vector>
#if (_MSC_VER <= 1500)
#define __STL_ARRAY_NOT_SUPPORT__
#endif
#ifndef __STL_ARRAY_NOT_SUPPORT__
#include <array>
#define KDTreeArray      std::vector<std::array<T, dim>>
typedef std::vector<std::array<int, 2>> KDTreeArrayInt2;
typedef std::vector<std::array<float, 2>> KDTreeArrayfloat2;
#endif



typedef struct __declspec(align(8))  _float2
{
	float x;
	float y;
}_float2;
typedef struct __declspec(align(8))  _int2
{
	unsigned int x;
	unsigned int y;
}_int2;

#ifndef BYTE
typedef unsigned char BYTE;
#endif
#ifndef USHORT
typedef unsigned short USHORT;
#endif // !USHORT

template  <typename T> class  Point2;
template <typename U> class Point3;
template <typename T> class  ImgInfo;
class _rect;
typedef Point2<int> Point2D;
typedef Point2<float> Point2Df;
typedef Point2<double> Point2Dlf;
typedef Point3<int> Point3D;
typedef Point3<float> Point3Df;
typedef Point3<double> Point3Dlf;

typedef ImgInfo<BYTE> ImageInfo8U;
typedef ImgInfo<BYTE[3]> ImageInfo8UC3;
typedef ImgInfo<USHORT> ImageInfo16U;
typedef ImgInfo<int> ImageInfo32S;
typedef ImgInfo<float> ImageInfoReal;
typedef ImgInfo<_float2> ImageInfoComplex;

typedef ImgInfo<BYTE> ImageInfo8U;
typedef ImgInfo<USHORT> ImageInfo16U;
typedef ImgInfo<float> ImageInfoReal;
typedef ImgInfo<_float2> ImageInfoComplex;

typedef std::vector<_rect> ArrRect;
typedef std::vector<Point2D> ArrPoint2D;
typedef std::vector<Point2Df> ArrPoint2Df;
typedef std::vector<Point3D> ArrPoint3D;
typedef std::vector<Point3Df> ArrPoint3Df;
typedef std::vector<int> arrInt;
typedef std::vector<_int2> arrInt2;
typedef std::vector<int>::iterator arrIntIter;


typedef void(*ClearProc1)(void *);
typedef int(*ClearProc2)(void *);
typedef bool(*ClearProc3)(void *);
typedef int(*memoryAllocatorProc1)(void **dst, __int64 nSize);
typedef void* (*memoryAllocatorProc2)(size_t nSize);



template  <typename T> class  Point2
{
public:
	Point2()
	{
		X = Y = 0;
	}

	Point2(const Point2 &point)
	{
		X = point.X;
		Y = point.Y;
	}


	Point2(T x, T y)
	{
		this->X = x;
		this->Y = y;
	}

	Point2 operator+(const Point2& point) const
	{
		return Point2D(X + point.X,
			Y + point.Y);
	}

	Point2 operator-(const Point2& point) const
	{
		return Point2(X - point.X,
			Y - point.Y);
	}

	int Equals(const Point2& point)
	{
		return (X == point.X) && (Y == point.Y);
	}

public:

	T X;
	T Y;
};

template  <typename U> class  Point3 : public Point2<U>
{
public:
	Point3() : Point2<U>()
	{
		Z = 0;
	}

	Point3(const Point3 &point) : Point2<U>(point)
	{
		Z = point.Z;
	}


	Point3(U x, U y, U z) : Point2<U>(x, y)
	{
		this->Z = z;
	}

	Point3 operator+(const Point3& point) const
	{
		return Point3(this->X + point.X, this->Y + point.Y, Z + point.Z);
	}

	Point3 operator-(const Point3& point) const
	{
		return Point3(this->X - point.X, this->Y - point.Y, Z - point.Z);
	}
	int Equals(const Point3& point)
	{
		return (this->X == point.X) && (this->Y == point.Y) && (Z == point.Z);
	}
	bool operator<(const Point3& point) const
	{
		return ((__int64(this->X) << 32) + (this->Y << 16) + this->Z) < ((__int64(point.X) << 32) + (point.Y << 16) + point.Z);
	}

public:
	U Z;
};
typedef class _rect
{
public:
	_rect()
	{
		row1 = row2 = col1 = col2 = 0;
	}
	_rect(int row1, int col1, int row2, int col2)
	{
		this->row1 = row1;
		this->col1 = col1;
		this->row2 = row2;
		this->col2 = col2;
	}
	int Width()
	{
		return col2 - col1 + 1;
	}
	int Height()
	{
		return row2 - row1 + 1;
	}
	__int64 Area()
	{
		return (__int64)Width()*(__int64)Height();
	}
	void roiShift(Point2D p)
	{
		row1 = row1 + p.Y;
		row2 = row2 + p.Y;
		col1 = col1 + p.X;
		col2 = col2 + p.X;
	}
	void roiShift(int x, int y)
	{
		row1 = row1 + y;
		row2 = row2 + y;
		col1 = col1 + x;
		col2 = col2 + x;
	}
	void roiExpand(Point2D p)
	{
		row1 = row1 - p.Y;
		row2 = row2 + p.Y;
		col1 = col1 - p.X;
		col2 = col2 + p.X;
	}
	int row1;
	int col1;
	int row2;
	int col2;
	bool operator ==(_rect &ref)
	{
		return isEquRect(ref);
	}
	bool operator !=(_rect &ref)
	{
		return !isEquRect(ref);
	}
	int isEquRect(_rect & ref)
	{
		if (ref.Width() == this->Width() && ref.Height() == this->Height())
		{
			return 1;
		}
		return 0;
	}
	int isInRect(Point2D pt)
	{
		if (row1 > pt.Y || row2 < pt.Y || col1 > pt.X || col2 < pt.X)
		{
			return 1;
		}
		return 0;
	}
	int isInRect(_rect rect)
	{
		if (row1 > rect.row1 || row2 < rect.row2 || col1 > rect.col1 || col2 < rect.col2)
		{
			return 0;
		}
		return 1;
	}

	_rect Intersection(_rect rect)
	{
		_rect r;
		r.col1 = (col1 > rect.col1) ? col1 : rect.col1;
		r.col2 = (col2 < rect.col2) ? col2 : rect.col2;
		r.row1 = (row1 > rect.row1) ? row1 : rect.row1;
		r.row2 = (row2 < rect.row2) ? row2 : rect.row2;

		if (r.Width() > 1 && r.Height() > 1 && isInRect(r))
			return r;
		else
			return _rect();

	}

	//Point2D X = Column Y = Row
	Point2D GetTopLeft()
	{
		return Point2D(col1, row1);
	}
	Point2D GetBottomRight()
	{
		return Point2D(col2, row2);
	}


}_rect;
struct CamParam
{
	float camParam[8];
};
template <typename T> class  ImgInfo
{
public:
	ImgInfo()
	{
		imgPtr = 0;
		Width = Height = WStep = 0;
		p1 = NULL;
		p2 = NULL;
		p3 = NULL;
	}
	ImgInfo(T* imgPtr, int w, int h, int WStep)
	{
		if (WStep < w)
			WStep = w;
		SetInfo(imgPtr, w, h, WStep);
		p1 = NULL;
		p2 = NULL;
		p3 = NULL;
	}
	~ImgInfo() {
		ClearBuffer();
	}
	ImgInfo(const ImgInfo &p)
	{
		memcpy(this, &p, sizeof(ImgInfo));
		p1 = NULL;
		p2 = NULL;
		p3 = NULL;
	}
	ImgInfo & operator =(const ImgInfo &p)
	{
		ClearBuffer();
		memcpy(this, &p, sizeof(ImgInfo));
		p1 = NULL;
		p2 = NULL;
		p3 = NULL;
		return *this;
	}
	void ClearBuffer()
	{
		if ((p1 || p2 || p3) && imgPtr)
		{
			if (p1)
				p1(imgPtr);
			else if (p2)
				p2(imgPtr);
			else if (p3)
				p3(imgPtr);
			p1 = NULL;
			p2 = NULL;
			p3 = NULL;
			imgPtr = NULL;
		}
	}
	void SetBuffer(int Val)
	{
		if (imgPtr)
			memset(imgPtr, Val, GetImageBufferSize());
	}
	void RoiFull()
	{
		this->roi.col1 = 0;
		this->roi.row1 = 0;
		if (this->Width > 0)
			this->roi.col2 = this->Width - 1;
		else
			this->roi.col2 = 0;
		if (this->Height > 0)
			this->roi.row2 = this->Height - 1;
		else
			this->roi.row2 = 0;
	}
	void SetRoi(_rect r)
	{
		roi.row1 = r.row1 < 0 ? 0 : r.row1 >= (Height - 1) ? (Height - 1) : r.row1;
		roi.col1 = r.col1 < 0 ? 0 : r.col1 >= (Width - 1) ? (Width - 1) : r.col1;
		roi.row2 = r.row2 < roi.row1 ? roi.row1 : r.row2 >= (Height - 1) ? (Height - 1) : r.row2;
		roi.col2 = r.col2 < roi.col1 ? roi.col1 : r.col2 >= (Width - 1) ? (Width - 1) : r.col2;
		if (roi.row1 >= roi.row2 || roi.col1 >= roi.col2)
		{
			roi.row1 = 0;
			roi.col1 = 0;
			roi.row2 = 0;
			roi.col2 = 0;

		}
	}
	void SetRoi(int ntop, int nleft, int nWidth, int nHeight)
	{
		_rect r(ntop, nleft, ntop + nHeight - 1, nleft + nWidth - 1);
		roi.row1 = r.row1 < 0 ? 0 : r.row1 >= (Height - 1) ? (Height - 1) : r.row1;
		roi.col1 = r.col1 < 0 ? 0 : r.col1 >= (Width - 1) ? (Width - 1) : r.col1;
		roi.row2 = r.row2 < roi.row1 ? roi.row1 : r.row2 >= (Height - 1) ? (Height - 1) : r.row2;
		roi.col2 = r.col2 < roi.col1 ? roi.col1 : r.col2 >= (Width - 1) ? (Width - 1) : r.col2;
	}
	void SetRoi(Point2D TopLeft, int nWidth, int nHeight)
	{
		_rect r(TopLeft.Y, TopLeft.X, TopLeft.Y + nHeight - 1, TopLeft.X + nWidth - 1);
		roi.row1 = r.row1 < 0 ? 0 : r.row1 >= (Height - 1) ? (Height - 1) : r.row1;
		roi.col1 = r.col1 < 0 ? 0 : r.col1 >= (Width - 1) ? (Width - 1) : r.col1;
		roi.row2 = r.row2 < roi.row1 ? roi.row1 : r.row2 >= (Height - 1) ? (Height - 1) : r.row2;
		roi.col2 = r.col2 < roi.col1 ? roi.col1 : r.col2 >= (Width - 1) ? (Width - 1) : r.col2;
	}
	void SetRoiSidMargin(int sideMargin)
	{
		roi.row1 = roi.row1 < sideMargin ? sideMargin : roi.row1;
		roi.col1 = roi.col1 < sideMargin ? sideMargin : roi.col1;
		roi.row2 = roi.row2 > (Height - 1) - sideMargin ? (Height - 1) - sideMargin : roi.row2;
		roi.col2 = roi.col2 > (Width - 1) - sideMargin ? (Width - 1) - sideMargin : roi.col2;
		SetRoi(roi);
	}
	void roiShift(Point2D p)
	{
		roi.roiShift(p);
		SetRoi(roi);
	}
	void roiShift(int x, int y)
	{
		roi.roiShift(x, y);
		SetRoi(roi);
	}
	void SetInfo(T* imgPtr, int w, int h, int WStep)
	{
		this->imgPtr = imgPtr;
		this->Width = w;
		this->Height = h;
		this->WStep = WStep;
		RoiFull();
	}
	void roiExpand(Point2D p)
	{
		roi.roiExpand(p);
		SetRoi(roi);
	}
	int GetImageWidth()
	{
		return Width;
	}
	int GetImageHeight()
	{
		return Height;
	}
	void GetRoiSize(int &W, int &H)
	{
		W = GetRoiWidth();
		H = GetRoiHeight();
	}
	void GetImageSize(int &W, int &H)
	{
		W = Width;
		H = Height;
	}
	int GetRoiWidth()
	{
		return roi.Width();
	}
	int GetRoiHeight()
	{
		return roi.Height();
	}
	T* GetRoiTopAddress()
	{
		return imgPtr + (roi.row1*__int64(WStep) + roi.col1);
	}
	T* GetRoiTopAddress(int row1, int col1)
	{
		return imgPtr + (row1*__int64(WStep) + col1);
	}
	T* GetRoiTopAddress(Point2D p)
	{
		return imgPtr + (p.Y*__int64(WStep) + p.X);
	}
	size_t GetImageBufferSize()
	{
		return __int64(WStep) * Height * sizeof(T);
	}
	int SetClearProc(ClearProc1 p)
	{
		if (p1 || p2 || p3)
		{
			return -1;
		}
		this->p1 = p;
		return 0;
	}
	int SetClearProc(ClearProc2 p)
	{
		if (p1 || p2 || p3)
		{
			return -1;
		}
		this->p2 = p;
		return 0;
	}
	int SetClearProc(ClearProc3 p)
	{
		if (p1 || p2 || p3)
		{
			return -1;
		}
		this->p3 = p;
	}
	void GenImageConst(int w, int h, int wstep)
	{
		ClearBuffer();
		if (wstep < w)
			wstep = w;
		SetInfo(0, w, h, wstep);
		this->imgPtr = (T*)malloc(__int64(wstep)*h * sizeof(T));
		SetClearProc(free);
	}
	int GenImageConst(_rect __roi)
	{
		ClearBuffer();
		SetInfo(0, __roi.Width(), __roi.Height(), __roi.Width());
		if (__roi.Area() == 1)
			return 0;
		this->imgPtr = (T*)malloc(__roi.Area() * sizeof(T));
		if (this->imgPtr == NULL)
			return 0;
		SetClearProc(free);
		return 1;
	}
	void GenImageConst(int w, int h, int wstep, memoryAllocatorProc2 AllocProc, ClearProc1 p)
	{
		ClearBuffer();
		SetInfo(0, w, h, wstep);
		this->imgPtr = (T*)AllocProc(__int64(wstep)*h * sizeof(T));
		SetClearProc(p);

	}
	void GenImageConst(int w, int h, int wstep, memoryAllocatorProc1 AllocProc, ClearProc2 p)
	{
		ClearBuffer();
		SetInfo(0, w, h, wstep);
		AllocProc((void**)&imgPtr, __int64(wstep)*h * sizeof(T));
		SetClearProc(p);
	}
	int isEquRect(ImgInfo &refImage)
	{
		return roi.isEquRect(refImage.roi);
	}
	void Set2D(int x, int y, T val)
	{
		if (x >= 0 && x < Width && y >= 0 && y < Height && imgPtr != NULL)
		{
			*(imgPtr + y * WStep + x) = val;
		}
	}
    void DrawRect(int left, int top, int right, int bottom, int color, bool fill)
	{
        if (left >= right || top >= bottom)
            return;

        if (fill)
        {
            for (int y = bottom; y >= top; --y)
                for (int x = right; x >= left; --x)
                    Set2D(x, y, color);
        }
        else
        {
            for (int x = right; x >= left; --x)
            {
                Set2D(x, top, color);
                Set2D(x, bottom, color);
            }

            for (int y = bottom; y >= top; --y)
            {
                Set2D(left, y, color);
                Set2D(right, y, color);
            }
        }
	}
	void CopyImage();

	T* imgPtr;
	int Width;
	int Height;
	int WStep;
	_rect roi;

protected:
	ClearProc1 p1;
	ClearProc2 p2;
	ClearProc3 p3;
};

#endif
