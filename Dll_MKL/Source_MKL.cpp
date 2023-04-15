#include "pch.h"
//#include "mkl.h"
#include "C:/Program Files (x86)/Intel/oneAPI/mkl/include/mkl.h"

extern "C"  _declspec(dllexport)
void CubeInterpolate(MKL_INT nx, MKL_INT ny, double* x, double* y,
	double* scoeff, MKL_INT nsite, double* site,
	MKL_INT ndorder, MKL_INT * dorder,
	double* result, int& ret, double* intEdges, double* integral,
	double* edgeDerivs, bool isUniform)
{
	try
	{
		int status;
		DFTaskPtr task;
		const double* intLedge = &intEdges[0];
		const double* intRedge = &intEdges[1];

		status = dfdNewTask1D(&task, nx, x, isUniform
			? DF_UNIFORM_PARTITION : DF_NON_UNIFORM_PARTITION, ny, y, DF_MATRIX_STORAGE_ROWS);
		if (status != DF_STATUS_OK)
		{
			ret = 1; return;
		}
		status = dfdEditPPSpline1D(task, DF_PP_CUBIC, DF_PP_NATURAL,
			DF_BC_1ST_LEFT_DER | DF_BC_1ST_RIGHT_DER,
			edgeDerivs, DF_NO_IC, NULL, scoeff, DF_NO_HINT);
		if (status != DF_STATUS_OK)
		{
			ret = 2; return;
		}

		status = dfdConstruct1D(task, DF_PP_SPLINE, DF_METHOD_STD);
		if (status != DF_STATUS_OK)
		{
			ret = 3; return;
		}

		status = dfdInterpolate1D(task, DF_INTERP, DF_METHOD_PP, nsite, site, isUniform
			? DF_UNIFORM_PARTITION : DF_NON_UNIFORM_PARTITION,
			ndorder, dorder, NULL, result, DF_MATRIX_STORAGE_ROWS, NULL);
		if (status != DF_STATUS_OK)
		{
			ret = 4; return;
		}

		status = dfdIntegrate1D(task, DF_METHOD_PP, 1, intLedge, DF_SORTED_DATA, intRedge,
			DF_SORTED_DATA, NULL, NULL, integral, DF_MATRIX_STORAGE_ROWS);
		if (status != DF_STATUS_OK)
		{
			ret = 5; return;
		}

		status = dfDeleteTask(&task);
		if (status != DF_STATUS_OK)
		{
			ret = 6; return;
		}

		ret = 0;
	}
	catch (...)
	{
		ret = 7;
	}
}