namespace apbd_int_cw5.SqlUtils
{
    public class SqlUtils
    {
        public static string DB_ADDRESS = "Data Source=db-mssql;Initial Catalog=s17174;Integrated Security=True";

        public static string DB_QUERRY_GET_ENROLMENT_BY_NAME_AND_STUDY_ID =
            "SELECT IdEnrollment, Semester, StartDate, E.IdStudy " +
            "FROM Enrollment E " +
            "JOIN Studies S " +
            "ON S.IdStudy = E.IdStudy " +
            "WHERE Semester = @semester AND S.Name = @name";

        public static string DB_INSERT_STUDENT =
            "INSERT INTO Students(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) " +
            "VALUES(@index, @name, @LastName, @BirthDate, @EnrollmentId)";

        public static string DB_GET_STUDY_ID_BY_STUDY_NAME =
            "SELECT IdStudy " +
            "FROM Studies " +
            "WHERE Name = @StudyName";

        public static string DB_SELECT_ALL_FROM_STUDENTS_BY_INDEX_NUMBER =
            "SELECT * " +
            "FROM Student " +
            "WHERE IndexNumber = @IndexNumber";

        public static string DB_INSERT_ENROLMENT =
            "INSERT INTO Enrollment (IdEnrollment, IdStudy, Semester, StartDate) " +
            "VALUES (@EnrollmentId, @IdStudies, @Semester, @TodayDate)";

        public static string DB_SELECT_ALL_FROM_ENROLMENT_BY_SEMESTER_AND_STUDIES_ID =
            "SELECT * " +
            "FROM Enrollment " +
            "WHERE Semester = @Semester AND IdStudy = @IdStudy";

        public static string DB_SELECT_ALL_FROM_ENROLLMENT_JOIN__ID =
            "SELECT e.Semester " +
            "FROM Enrollment e " +
            "JOIN Student s " +
            "ON e.IdEnrollment = s.IdEnrollment " +
            "WHERE s.IndexNumber = @id;";

        public static string DB_SELECT_ALL_WHERE_ID =
            "SELECT * " +
            "FROM Student " +
            "WHERE IndexNumber = @index";

        public static string DB_SELECT_ALL_FROM_STUDENT =
            "SELECT * " +
            "FROM Student";

        public static string DB_SELECT_MAX_ENROLLMENT =
            "SELECT MAX(IdEnrollment) " +
            "AS EnrolmentMaxId " +
            "FROM Enrollment " +
            "WHERE Semester = @Semester";

        public static string DB_SELECT_ALL_ENROLL_WITH_ID =
            "SELECT * " +
            "FROM Enrollment " +
            "WHERE IdEnrollment = @IdEnrollment";
    }

}
