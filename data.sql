-- Dummy data for school_class table
INSERT INTO school_class (class_name, teacher_name, room_number, class_capacity)
VALUES 
('Mathematics', 'Mr. Smith', 'Room 101', 30),
('Science', 'Ms. Johnson', 'Room 102', 25),
('History', 'Mr. Thompson', 'Room 103', 20),
('History', 'Mr. Thompson', 'Room 103', 20),
('Math', 'Mr. Johnson', 'Room 105', 18),
('Science', 'Ms. Davis', 'Room 201', 22),
('English', 'Mrs. Wilson', 'Room 304', 19),
('Geography', 'Mr. Martinez', 'Room 207', 17),
('Art', 'Ms. Brown', 'Room 306', 21),
('Music', 'Mr. Garcia', 'Room 101', 23),
('Physical Education', 'Mr. Smith', 'Gym', 20),
('Computer Science', 'Mrs. Jones', 'Computer Lab', 19),
('Spanish', 'Ms. Thomas', 'Room 202', 18);

-- Dummy data for school_student table
INSERT INTO school_student (student_name, date_of_birth, class_id)
VALUES 
('John Doe', '2008-05-10', 1),
('Jane Smith', '2009-07-15', 2),
('Michael Johnson', '2008-12-20', 1),
('Emily Thompson', '2009-03-05', 3),
('John Doe', '2008-05-10', 1),
('Jane Smith', '2010-08-15', 3),
('Michael Johnson', '2009-12-20', 2),
('Emily Brown', '2011-02-25', 5),
('David Martinez', '2007-07-08', 4),
('Sarah Wilson', '2012-09-30', 6),
('James Jones', '2006-04-12', 7),
('Jessica Taylor', '2013-11-05', 9),
('Christopher White', '2014-06-18', 8),
('Amanda Anderson', '2005-10-22', 10);

-- Dummy data for school_subject table
INSERT INTO school_subject (subject_name, teacher_name)
VALUES 
('Algebra', 'Mr. Brown'),
('Biology', 'Ms. Davis'),
('World History', 'Mr. Wilson'),
('Algebra', 'Mr. Brown'),
('Geometry', 'Mrs. Davis'),
('Calculus', 'Mr. Garcia'),
('Statistics', 'Ms. Wilson'),
('Trigonometry', 'Mr. Martinez'),
('Linear Algebra', 'Mrs. Thompson'),
('Differential Equations', 'Mr. Harris'),
('Number Theory', 'Ms. Thomas'),
('Discrete Mathematics', 'Mr. Clark'),
('Topology', 'Mrs. Rodriguez');

-- Dummy data for school_result table
INSERT INTO school_result (student_id, subject_id, score)
VALUES 
(1, 1, 85.5),
(2, 2, 90),
(3, 1, 88),
(4, 3, 92.5),
(1, 1, 85.5),
(2, 1, 90.0),
(3, 2, 78.3),
(4, 2, 87.9),
(5, 3, 92.1),
(6, 3, 88.5),
(7, 4, 95.2),
(8, 4, 91.7),
(9, 5, 86.4),
(10, 5, 89.8);
