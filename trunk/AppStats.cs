using System;
using System.Collections;

namespace ShipScrn
{
    public class AppStats
    {
        int nPacksFile;
        int nTrackingNumbersFile;
        int nPacksProcessed;
        int nPacksSkipped;
        int nPacksInvoiced;
        int nPacksTrackOnly;
        int nTrackingNumbersProcessed;
        decimal totalFreightFile;
        decimal totalWeightFile;
        decimal totalHandlingBilled;
        decimal totalFreightBilled;
        decimal totalWeightProcessed;
        public AppStats()
        {
            InitializeVars();
        }
        void InitializeVars()
        {
            nPacksFile = 0;
            nTrackingNumbersFile = 0;
            nPacksProcessed = 0;
            nPacksSkipped = 0;
            nPacksInvoiced = 0;
            nPacksTrackOnly = 0;
            nTrackingNumbersProcessed = 0;
            totalFreightFile = 0.0m;
            totalWeightFile = 0.0m;
            totalHandlingBilled = 0.0m;
            totalFreightBilled = 0.0m;
            totalWeightProcessed = 0.0m;
        }
        public int NPacksFile
        {
            get
            {
                return nPacksFile;
            }
            set
            {
                nPacksFile = value;
            }
        }
        public int NTrackingNumbersFile
        {
            get
            {
                return nTrackingNumbersFile;
            }
            set
            {
                nTrackingNumbersFile = value;
            }
        }
        public int NPacksProcessed
        {
            get
            {
                return nPacksProcessed;
            }
            set
            {
                nPacksProcessed = value;
            }
        }
        public int NTrackingProcessed
        {
            get
            {
                return nTrackingNumbersProcessed;
            }
            set
            {
                nTrackingNumbersProcessed = value;
            }
        }
        public int NPacksInvoiced
        {
            get
            {
                return nPacksInvoiced;
            }
            set
            {
                nPacksInvoiced = value;
            }
        }
        public decimal TotalFreightBilled
        {
            get
            {
                return totalFreightBilled;
            }
            set
            {
                totalFreightBilled = value;
            }
        }
        public decimal TotalFreightFile
        {
            get
            {
                return totalFreightFile;
            }
            set
            {
                totalFreightFile = value;
            }
        }
        public decimal TotalWeightFile
        {
            get
            {
                return totalWeightFile;
            }
            set
            {
                totalWeightFile = value;
            }
        }

    }
}