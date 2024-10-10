#pragma once

class TreeBuffer;
class LIBIY_EXT_CLASS BlobGenerator {

public:
    BlobGenerator(int *srcArr, const int srcnItems, int nDist,int maxBlobs);
    ~BlobGenerator() { releaseBuffer(); };
    const unsigned getNextBlob(int **ArrInt3);
    void rewind(void);

private:
    void genBlobs(int *srcArr, const int srcnItems);
    void releaseBuffer(void);

private:
    TreeBuffer *m_treeBuffer;
private:
    int m_nMaxBlobs;
    int m_nBlobs;
    int m_currIndex;
    int m_nDist;

};
