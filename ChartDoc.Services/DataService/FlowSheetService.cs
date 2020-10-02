using ChartDoc.DAL;
using ChartDoc.Models;
using ChartDoc.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace ChartDoc.Services.DataService
{
    /// <summary>
    /// FlowSheetService
    /// </summary>
    public class FlowSheetService : IFlowSheetService
    {
        #region Instance Variable******************************************************************************************************************************
        DBUtils db = DBUtils.GetInstance;
        #endregion

        #region FlowSheetService Constructor*******************************************************************************************************************************
        /// <summary>
        /// FlowSheetService : Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public FlowSheetService(IConfiguration configuration)
        {
            db._configaration = configuration;
        }
        #endregion

        #region Get Appointment********************************************************************************************************************************
        /// <summary>
        /// GetAppointment
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>List<clsFlowSheet></returns>
        public List<clsFlowSheet> GetAppointment(string date)
        {
            List<clsFlowSheet> flowSheetList = new List<clsFlowSheet>();
            DataTable dtAppointment = BindAppointment(date);
            for (int index = 0; index <= dtAppointment.Rows.Count - 1; index++)
            {
                if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]), "Appointment") == 0 || String.IsNullOrWhiteSpace(Convert.ToString(dtAppointment.Rows[index]["FlowArea"])))
                {
                    flowSheetList.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                            patientId = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                            colorCode = Convert.ToString(dtAppointment.Rows[index]["COLORCODE"]),
                            phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                            email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                            dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                            gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                            address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                            serviceId = Convert.ToString(dtAppointment.Rows[index]["ServiceID"]),
                            note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtAppointment.Rows[index]["filePath"])
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty
                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientID = string.Empty,
                            patientName = string.Empty
                        }
                    });
                }
                else if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "WAITING") == 0)
                {
                    flowSheetList.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            colorCode = string.Empty,
                            serviceId = string.Empty
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                            patientId = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                            phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                            email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                            dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                            gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                            address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                            note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtAppointment.Rows[index]["filePath"])

                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientID = string.Empty,
                            patientName = string.Empty
                        }
                    });
                }
                else if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "ENCOUNTER") == 0)
                {
                    flowSheetList.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            colorCode = string.Empty,
                            serviceId = string.Empty
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty
                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                            patientId = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                            phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                            email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                            dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                            gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                            address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                            note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtAppointment.Rows[index]["filePath"])
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientID = string.Empty,
                            patientName = string.Empty
                        }
                    });
                }
                else if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "FINISH") == 0)
                {
                    flowSheetList.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            colorCode = string.Empty,
                            serviceId = string.Empty
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty
                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                            patientID = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                            phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                            email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                            dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                            gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                            address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                            note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtAppointment.Rows[index]["filePath"])

                        }
                    });
                }
            }

            return flowSheetList;
        }

        #region BindAppointment********************************************************************************************************************************
        /// <summary>
        /// BindAppointment
        /// </summary>
        /// <param name="pDate">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindAppointment(string pDate)
        {
            DataTable dtAppointment = new DataTable();
            string sqlAppointment = "USP_FETCHAPPOINTMENT '" + pDate + "'";
            dtAppointment = db.GetData(sqlAppointment);
            return dtAppointment;
        }
        #endregion

        #endregion

        #region Get FlowSheet Details**************************************************************************************************************************
        /// <summary>
        /// GetFlowsheet
        /// </summary>
        /// <param name="date">string</param>
        /// <param name="doctorId">string</param>
        /// <returns>List<clsFlowSheet></returns>
        public List<clsFlowSheet> GetFlowsheet(string date, string doctorId)
        {
            List<clsFlowSheet> lstFlowSheet = new List<clsFlowSheet>();
            DataTable dtFlowsheet = BindFlowsheet(date, doctorId);
            for (int index = 0; index <= dtFlowsheet.Rows.Count - 1; index++)
            {
                if (string.Compare(Convert.ToString(dtFlowsheet.Rows[index]["FlowArea"]).ToUpper(), "APPOINTMENT") == 0)
                {
                    lstFlowSheet.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = Convert.ToInt32(dtFlowsheet.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtFlowsheet.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtFlowsheet.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtFlowsheet.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtFlowsheet.Rows[index]["ToTime"]),
                            patientId = Convert.ToString(dtFlowsheet.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtFlowsheet.Rows[index]["PatientName"]),
                            colorCode = Convert.ToString(dtFlowsheet.Rows[index]["COLORCODE"]),
                            isReady = Convert.ToString(dtFlowsheet.Rows[index]["IsReady"]),
                            serviceId = Convert.ToString(dtFlowsheet.Rows[index]["ServiceID"]),
                            dateOfBirth = Convert.ToString(dtFlowsheet.Rows[index]["DOB"]),
                            gender = Convert.ToString(dtFlowsheet.Rows[index]["Gender"]),
                            phoneNo = Convert.ToString(dtFlowsheet.Rows[index]["Mob_No"]),
                            email = Convert.ToString(dtFlowsheet.Rows[index]["Email"]),
                            address = Convert.ToString(dtFlowsheet.Rows[index]["Add_Line"]),
                            note = Convert.ToString(dtFlowsheet.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtFlowsheet.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtFlowsheet.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtFlowsheet.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtFlowsheet.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtFlowsheet.Rows[index]["filePath"])
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientID = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        }
                    });
                }
                else if (string.Compare(Convert.ToString(dtFlowsheet.Rows[index]["FlowArea"]).ToUpper(), "WAITING") == 0)
                {
                    lstFlowSheet.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty,
                            colorCode = string.Empty,
                            isReady = "0",
                            serviceId = string.Empty
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = Convert.ToInt32(dtFlowsheet.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtFlowsheet.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtFlowsheet.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtFlowsheet.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtFlowsheet.Rows[index]["ToTime"]),
                            patientId = Convert.ToString(dtFlowsheet.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtFlowsheet.Rows[index]["PatientName"]),
                            serviceId = Convert.ToString(dtFlowsheet.Rows[index]["ServiceID"]),
                            dateOfBirth = Convert.ToString(dtFlowsheet.Rows[index]["DOB"]),
                            gender = Convert.ToString(dtFlowsheet.Rows[index]["Gender"]),
                            phoneNo = Convert.ToString(dtFlowsheet.Rows[index]["Mob_No"]),
                            email = Convert.ToString(dtFlowsheet.Rows[index]["Email"]),
                            address = Convert.ToString(dtFlowsheet.Rows[index]["Add_Line"]),
                            note = Convert.ToString(dtFlowsheet.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtFlowsheet.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtFlowsheet.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtFlowsheet.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtFlowsheet.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtFlowsheet.Rows[index]["filePath"])
                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientID = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        }
                    });
                }
                else if (string.Compare(Convert.ToString(dtFlowsheet.Rows[index]["FlowArea"]).ToUpper(), "ENCOUNTER") == 0)
                {
                    lstFlowSheet.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty,
                            colorCode = string.Empty,
                            serviceId = string.Empty
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty
                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = Convert.ToInt32(dtFlowsheet.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtFlowsheet.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtFlowsheet.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtFlowsheet.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtFlowsheet.Rows[index]["ToTime"]),
                            patientId = Convert.ToString(dtFlowsheet.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtFlowsheet.Rows[index]["PatientName"]),
                            serviceId = Convert.ToString(dtFlowsheet.Rows[index]["ServiceID"]),
                            dateOfBirth = Convert.ToString(dtFlowsheet.Rows[index]["DOB"]),
                            gender = Convert.ToString(dtFlowsheet.Rows[index]["Gender"]),
                            phoneNo = Convert.ToString(dtFlowsheet.Rows[index]["Mob_No"]),
                            email = Convert.ToString(dtFlowsheet.Rows[index]["Email"]),
                            address = Convert.ToString(dtFlowsheet.Rows[index]["Add_Line"]),
                            note = Convert.ToString(dtFlowsheet.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtFlowsheet.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtFlowsheet.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtFlowsheet.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtFlowsheet.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtFlowsheet.Rows[index]["filePath"])
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientID = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        }
                    });
                }
                else if (string.Compare(Convert.ToString(dtFlowsheet.Rows[index]["FlowArea"]).ToUpper(), "FINISH") == 0)
                {
                    lstFlowSheet.Add(new clsFlowSheet
                    {
                        schduleAppoinment = new clsScheduleAppointment
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty,
                            colorCode = string.Empty,
                            serviceId = string.Empty
                        },
                        waitingArea = new clsWaitingArea
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        },
                        consultationRoom = new clsConsultationRoom
                        {
                            appointmentId = 0,
                            doctorId = string.Empty,
                            doctorName = string.Empty,
                            fromTime = string.Empty,
                            toTime = string.Empty,
                            patientId = string.Empty,
                            patientName = string.Empty,
                            dateOfBirth = string.Empty,
                            gender = string.Empty,
                            phoneNo = string.Empty,
                            email = string.Empty,
                            address = string.Empty,
                            note = string.Empty,
                            positionId = string.Empty,
                            reasonId = string.Empty,
                            reason = string.Empty,
                            appointmentFromTime = string.Empty,
                            appointmentToTime = string.Empty
                        },
                        checkingOut = new clsCheckingOut
                        {
                            appointmentId = Convert.ToInt32(dtFlowsheet.Rows[index]["AppointmentId"]),
                            doctorId = Convert.ToString(dtFlowsheet.Rows[index]["DoctorId"]),
                            doctorName = Convert.ToString(dtFlowsheet.Rows[index]["DoctorNAME"]),
                            date = Convert.ToString(date),
                            fromTime = Convert.ToString(dtFlowsheet.Rows[index]["FromTime"]),
                            toTime = Convert.ToString(dtFlowsheet.Rows[index]["ToTime"]),
                            patientID = Convert.ToString(dtFlowsheet.Rows[index]["PatientId"]),
                            patientName = Convert.ToString(dtFlowsheet.Rows[index]["PatientName"]),
                            dateOfBirth = Convert.ToString(dtFlowsheet.Rows[index]["DOB"]),
                            serviceId = Convert.ToString(dtFlowsheet.Rows[index]["ServiceID"]),
                            gender = Convert.ToString(dtFlowsheet.Rows[index]["Gender"]),
                            phoneNo = Convert.ToString(dtFlowsheet.Rows[index]["Mob_No"]),
                            email = Convert.ToString(dtFlowsheet.Rows[index]["Email"]),
                            address = Convert.ToString(dtFlowsheet.Rows[index]["Add_Line"]),
                            note = Convert.ToString(dtFlowsheet.Rows[index]["Note"]),
                            positionId = Convert.ToString(dtFlowsheet.Rows[index]["PositionID"]),
                            reasonId = Convert.ToString(dtFlowsheet.Rows[index]["ReasonID"]),
                            reason = Convert.ToString(dtFlowsheet.Rows[index]["Reason"]),
                            appointmentFromTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointFromTime"]),
                            appointmentToTime = Convert.ToString(dtFlowsheet.Rows[index]["AppointToTime"]),
                            roomNo = Convert.ToString(dtFlowsheet.Rows[index]["ROOMNO"]),
                            filePath = Convert.ToString(dtFlowsheet.Rows[index]["filePath"])
                        }
                    });
                }
            }
            return lstFlowSheet;
        }
        #endregion

        #region Get Office Calendar List***********************************************************************************************************************
        /// <summary>
        /// GetOfficeCalenderList
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>List<clsFlowSheet></returns>
        public List<clsFlowSheet> GetOfficeCalenderList(string date)
        {
            List<clsFlowSheet> lstFlowSheet = new List<clsFlowSheet>();
            DataTable dtAppointment = BindAppointmentList(date);
            for (int index = 0; index <= dtAppointment.Rows.Count - 1; index++)
            {
                if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "APPOINTMENT") == 0 || string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]), string.Empty) == 0)
                {
                    if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "APPOINTMENT") == 0 || string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]), string.Empty) == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(dtAppointment.Rows[index]["Date"]),
                                fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                                patientId = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                                colorCode = Convert.ToString(dtAppointment.Rows[index]["COLORCODE"]),
                                phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                                serviceId = Convert.ToString(dtAppointment.Rows[index]["ServiceID"]),
                                note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                                //filePath = Convert.ToString(dtAppointment.Rows[index]["filepath"])
                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientID = string.Empty,
                                patientName = string.Empty
                            }
                        });
                    }
                    else if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "WAITING") == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty,
                                colorCode = string.Empty,
                                serviceId = string.Empty

                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(date),
                                fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                                patientId = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                                phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                                note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                                //filePath = Convert.ToString(dtAppointment.Rows[index]["filepath"]),

                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientID = string.Empty,
                                patientName = string.Empty
                            }
                        });
                    }
                    else if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "ENCOUNTER") == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty,
                                colorCode = string.Empty,
                                serviceId = string.Empty
                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(date),
                                fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                                patientId = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                                phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                                note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                                //filePath = Convert.ToString(dtAppointment.Rows[index]["filepath"])
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientID = string.Empty,
                                patientName = string.Empty
                            }
                        });
                    }
                    else if (string.Compare(Convert.ToString(dtAppointment.Rows[index]["FlowArea"]).ToUpper(), "FINISH") == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty,
                                colorCode = string.Empty,
                                serviceId = string.Empty
                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = Convert.ToInt32(dtAppointment.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointment.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointment.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(date),
                                fromTime = Convert.ToString(dtAppointment.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointment.Rows[index]["ToTime"]),
                                patientID = Convert.ToString(dtAppointment.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointment.Rows[index]["PatientName"]),
                                phoneNo = Convert.ToString(dtAppointment.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointment.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointment.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointment.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointment.Rows[index]["Address"]),
                                note = Convert.ToString(dtAppointment.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointment.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointment.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointment.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointment.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointment.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointment.Rows[index]["ROOMNO"]),
                                //filePath = Convert.ToString(dtAppointment.Rows[index]["filepath"])

                            }
                        });
                    }
                }
            }
            return lstFlowSheet;
        }
        #endregion

        #region Appointment Weekly View************************************************************************************************************************
        /// <summary>
        /// AppointmentWeeklyView
        /// </summary>
        /// <param name="date">string</param>
        /// <param name="doctorId">string</param>
        /// <returns>List<clsFlowSheet></returns>
        public List<clsFlowSheet> AppointmentWeeklyView(string date, string doctorId)
        {
            List<clsFlowSheet> lstFlowSheet = new List<clsFlowSheet>();

            DataTable dtAppointmentWeekly = BindAppointmentWeekly(date, doctorId);
            for (int index = 0; index <= dtAppointmentWeekly.Rows.Count - 1; index++)
            {
               
                    if (string.Compare(Convert.ToString(dtAppointmentWeekly.Rows[index]["FlowArea"]).ToUpper(), "APPOINTMENT") == 0 ||
                        string.Compare(Convert.ToString(dtAppointmentWeekly.Rows[index]["FlowArea"]), string.Empty) == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = Convert.ToInt32(dtAppointmentWeekly.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(dtAppointmentWeekly.Rows[index]["Date"]),
                                fromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["ToTime"]),
                                patientId = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientName"]),
                                colorCode = Convert.ToString(dtAppointmentWeekly.Rows[index]["COLORCODE"]),
                                phoneNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointmentWeekly.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointmentWeekly.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointmentWeekly.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointmentWeekly.Rows[index]["Address"]),
                                serviceId = Convert.ToString(dtAppointmentWeekly.Rows[index]["ServiceID"]),
                                note = Convert.ToString(dtAppointmentWeekly.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointmentWeekly.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointmentWeekly.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointmentWeekly.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["ROOMNO"])

                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientID = string.Empty,
                                patientName = string.Empty
                            }
                        });
                    }
                    else if (string.Compare(Convert.ToString(dtAppointmentWeekly.Rows[index]["FlowArea"]).ToUpper(), "WAITING") == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty,
                                colorCode = string.Empty,
                                serviceId = string.Empty

                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = Convert.ToInt32(dtAppointmentWeekly.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(date),
                                fromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["ToTime"]),
                                patientId = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientName"]),
                                phoneNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointmentWeekly.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointmentWeekly.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointmentWeekly.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointmentWeekly.Rows[index]["Address"]),
                                note = Convert.ToString(dtAppointmentWeekly.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointmentWeekly.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointmentWeekly.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointmentWeekly.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["ROOMNO"])

                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientID = string.Empty,
                                patientName = string.Empty
                            }
                        });
                    }
                    else if (string.Compare(Convert.ToString(dtAppointmentWeekly.Rows[index]["FlowArea"]).ToUpper(), "ENCOUNTER") == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty,
                                colorCode = string.Empty,
                                serviceId = string.Empty
                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = Convert.ToInt32(dtAppointmentWeekly.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(date),
                                fromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["ToTime"]),
                                patientId = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientName"]),
                                phoneNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointmentWeekly.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointmentWeekly.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointmentWeekly.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointmentWeekly.Rows[index]["Address"]),
                                note = Convert.ToString(dtAppointmentWeekly.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointmentWeekly.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointmentWeekly.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointmentWeekly.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["ROOMNO"])
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientID = string.Empty,
                                patientName = string.Empty
                            }
                        });
                    }
                    else if (string.Compare(Convert.ToString(dtAppointmentWeekly.Rows[index]["FlowArea"]).ToUpper(), "FINISH") == 0)
                    {
                        lstFlowSheet.Add(new clsFlowSheet
                        {
                            schduleAppoinment = new clsScheduleAppointment
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty,
                                colorCode = string.Empty,
                                serviceId = string.Empty
                            },
                            waitingArea = new clsWaitingArea
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            consultationRoom = new clsConsultationRoom
                            {
                                appointmentId = 0,
                                doctorId = string.Empty,
                                doctorName = string.Empty,
                                fromTime = string.Empty,
                                toTime = string.Empty,
                                patientId = string.Empty,
                                patientName = string.Empty
                            },
                            checkingOut = new clsCheckingOut
                            {
                                appointmentId = Convert.ToInt32(dtAppointmentWeekly.Rows[index]["AppointmentId"]),
                                doctorId = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorId"]),
                                doctorName = Convert.ToString(dtAppointmentWeekly.Rows[index]["DoctorNAME"]),
                                date = Convert.ToString(date),
                                fromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["FromTime"]),
                                toTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["ToTime"]),
                                patientID = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientId"]),
                                patientName = Convert.ToString(dtAppointmentWeekly.Rows[index]["PatientName"]),
                                phoneNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["Phoneno"]),
                                email = Convert.ToString(dtAppointmentWeekly.Rows[index]["Email"]),
                                dateOfBirth = Convert.ToString(dtAppointmentWeekly.Rows[index]["DateofBirth"]),
                                gender = Convert.ToString(dtAppointmentWeekly.Rows[index]["Gender"]),
                                address = Convert.ToString(dtAppointmentWeekly.Rows[index]["Address"]),
                                note = Convert.ToString(dtAppointmentWeekly.Rows[index]["Note"]),
                                positionId = Convert.ToString(dtAppointmentWeekly.Rows[index]["PositionID"]),
                                reasonId = Convert.ToString(dtAppointmentWeekly.Rows[index]["ReasonID"]),
                                reason = Convert.ToString(dtAppointmentWeekly.Rows[index]["Reason"]),
                                appointmentFromTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointFromTime"]),
                                appointmentToTime = Convert.ToString(dtAppointmentWeekly.Rows[index]["AppointToTime"]),
                                roomNo = Convert.ToString(dtAppointmentWeekly.Rows[index]["ROOMNO"])

                            }
                        });
                    }
                
            }

            return lstFlowSheet;
        }
        #endregion

        #region Private Methods********************************************************************************************************************************
        /// <summary>
        /// BindFlowsheet
        /// </summary>
        /// <param name="pDate">string</param>
        /// <param name="doctorId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindFlowsheet(string pDate, string doctorId)
        {
            DataTable dtFlowsheet = new DataTable();
            string sqlFlowsheet = "USP_FlowSheet '" + pDate + "','" + doctorId + "'";
            dtFlowsheet = db.GetData(sqlFlowsheet);
            return dtFlowsheet;
        }

        /// <summary>
        /// BindAppointmentList
        /// </summary>
        /// <param name="pDate">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindAppointmentList(string pDate)
        {
            DataTable dtAppointments = new DataTable();
            string sqlAppointment = "USP_FETCHAPPOINTMENT_LIST '" + pDate + "'";
            dtAppointments = db.GetData(sqlAppointment);
            return dtAppointments;
        }

        /// <summary>
        /// BindAppointmentWeekly
        /// </summary>
        /// <param name="pDate">string</param>
        /// <param name="pDoctorId">string</param>
        /// <returns>DataTable</returns>
        private DataTable BindAppointmentWeekly(string pDate, string pDoctorId)
        {
            DataTable dtAppointmentWeekly = new DataTable();
            string sql = "USP_FETCHAPPOINTMENT_DATERANGE '" + pDate + "','" + pDoctorId + "'";
            dtAppointmentWeekly = db.GetData(sql);
            return dtAppointmentWeekly;
        }

        public string getInsurenaceStatus(string patientId)
        {
            string result = string.Empty;
            result = (string)db.GetSingleValue("USP_CHECKINSURANCE '" + patientId + "'" );
            return result;
        }
        #endregion
    }
}
