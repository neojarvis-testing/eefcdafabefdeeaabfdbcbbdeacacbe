-- Active: 1708412342472@@127.0.0.1@1433@appdb@dbo
CREATE TABLE school_class(  
    class_id int IDENTITY(1,1) primary key,
    class_name varchar(100) not null,
    teacher_name varchar(100) not null,
    room_number varchar(20),
    class_capacity int,
    create_time DATETIME default GETDATE(),
    update_time DATETIME default GETDATE()
);
EXECUTE sp_addextendedproperty N'MS_Description', 'Table for managing school class data', N'user', N'dbo', N'table', N'school_class', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', 'Unique identifier for each class', N'user', N'dbo', N'table', N'school_class', N'column', N'class_id';
EXECUTE sp_addextendedproperty N'MS_Description', 'Name of the class', N'user', N'dbo', N'table', N'school_class', N'column', N'class_name';
EXECUTE sp_addextendedproperty N'MS_Description', 'Name of the teacher', N'user', N'dbo', N'table', N'school_class', N'column', N'teacher_name';
EXECUTE sp_addextendedproperty N'MS_Description', 'Room number where the class is held', N'user', N'dbo', N'table', N'school_class', N'column', N'room_number';
EXECUTE sp_addextendedproperty N'MS_Description', 'Maximum number of students that can be enrolled in the class', N'user', N'dbo', N'table', N'school_class', N'column', N'class_capacity';
EXECUTE sp_addextendedproperty N'MS_Description', 'Timestamp for when the record was created', N'user', N'dbo', N'table', N'school_class', N'column', N'create_time';
EXECUTE sp_addextendedproperty N'MS_Description', 'Timestamp for when the record was last updated', N'user', N'dbo', N'table', N'school_class', N'column', N'update_time';






-- Table for managing school students
CREATE TABLE school_student(
    student_id int IDENTITY(1,1) primary key,
    student_name varchar(100) not null,
    date_of_birth DATE,
    class_id int references school_class(class_id),
    create_time DATETIME default GETDATE(),
    update_time DATETIME default GETDATE()
);
EXECUTE sp_addextendedproperty N'MS_Description', 'Table for managing school students', N'user', N'dbo', N'table', N'school_student', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', 'Unique identifier for each student', N'user', N'dbo', N'table', N'school_student', N'column', N'student_id';
EXECUTE sp_addextendedproperty N'MS_Description', 'Name of the student', N'user', N'dbo', N'table', N'school_student', N'column', N'student_name';
EXECUTE sp_addextendedproperty N'MS_Description', 'Date of birth of the student', N'user', N'dbo', N'table', N'school_student', N'column', N'date_of_birth';
EXECUTE sp_addextendedproperty N'MS_Description', 'ID of the class the student belongs to', N'user', N'dbo', N'table', N'school_student', N'column', N'class_id';
EXECUTE sp_addextendedproperty N'MS_Description', 'Timestamp for when the record was created', N'user', N'dbo', N'table', N'school_student', N'column', N'create_time';
EXECUTE sp_addextendedproperty N'MS_Description', 'Timestamp for when the record was last updated', N'user', N'dbo', N'table', N'school_student', N'column', N'update_time';

-- Table for managing school subjects
CREATE TABLE school_subject(
    subject_id int IDENTITY(1,1) primary key,
    subject_name varchar(100) not null,
    teacher_name varchar(100) not null,
    create_time DATETIME default GETDATE(),
    update_time DATETIME default GETDATE()
);
EXECUTE sp_addextendedproperty N'MS_Description', 'Table for managing school subjects', N'user', N'dbo', N'table', N'school_subject', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', 'Unique identifier for each subject', N'user', N'dbo', N'table', N'school_subject', N'column', N'subject_id';
EXECUTE sp_addextendedproperty N'MS_Description', 'Name of the subject', N'user', N'dbo', N'table', N'school_subject', N'column', N'subject_name';
EXECUTE sp_addextendedproperty N'MS_Description', 'Name of the teacher teaching the subject', N'user', N'dbo', N'table', N'school_subject', N'column', N'teacher_name';
EXECUTE sp_addextendedproperty N'MS_Description', 'Timestamp for when the record was created', N'user', N'dbo', N'table', N'school_subject', N'column', N'create_time';
EXECUTE sp_addextendedproperty N'MS_Description', 'Timestamp for when the record was last updated', N'user', N'dbo', N'table', N'school_subject', N'column', N'update_time';

-- Table for managing school results
CREATE TABLE school_result(
    result_id int IDENTITY(1,1) primary key,
    student_id int references school_student(student_id),
    subject_id int references school_subject(subject_id),
    score decimal(5, 2),
    create_time DATETIME default GETDATE(),
    update_time DATETIME default GETDATE()
);
EXECUTE sp_addextendedproperty N'MS_Description', 'Table for managing school results', N'user', N'dbo', N'table', N'school_result', NULL, NULL;
EXECUTE sp_addextendedproperty N'MS_Description', 'Unique identifier for each result', N'user', N'dbo', N'table', N'school_result', N'column', N'result_id';
EXECUTE sp_addextendedproperty N'MS_Description', 'ID of the student who achieved the result', N'user', N'dbo', N'table', N'school_result', N'column', N'student_id';
EXECUTE sp_addextendedproperty N'MS_Description', 'ID of the subject for which the result is achieved', N'user', N'dbo', N'table', N'school_result', N'column', N'subject_id';
EXECUTE sp_addextendedproperty N'MS_Description', 'Score achieved by the student in the subject', N'user', N'dbo', N'table', N'school_result', N'column', N'score';
EXECUTE sp_addextendedproperty N'MS_Description', 'Timestamp for when the record was created', N'user', N'dbo', N'table', N'school_result', N'column', N'create_time';


