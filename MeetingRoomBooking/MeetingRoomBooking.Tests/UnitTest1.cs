
using MeetingRoomBooking.Helpers;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Implementations;
using MeetingRoomBooking.Services.Interfaces;
using System.Linq.Expressions;

namespace MeetingRoomBooking.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void BackToBackBooking_AtEnd_ShouldReturnFalse()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(11, 0),
                EndTime = new TimeOnly(12, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.False(result);
        }
        [Fact]
        public void BackToBackBooking_AtBeginning_ShouldReturnFalse()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(10, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.False(result);
        }
        [Fact]
        public void ExactMatch_Overlap_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(10, 15),
                EndTime = new TimeOnly(11, 15)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);

        }
        [Fact]
        public void PartialOverlap_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(10, 30),
                EndTime = new TimeOnly(11, 30)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void PartialOverlap_AtBeginning_ShouldReturnTrue()
        {
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(9, 30),
                EndTime = new TimeOnly(10, 30)
            };
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void NewBookingContainsExisting_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(12, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void ExistingBookingContainsNew_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(12, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void NoOverlap_ShouldReturnFalse()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(1, 0),
                EndTime = new TimeOnly(2, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.False(result);



    }
        [Fact]
        public void CompleteInside_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(1, 0),
                EndTime = new TimeOnly(2, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(1, 15),
                EndTime = new TimeOnly(1, 45)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void CompleteEnclosing_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(13, 0),
                EndTime = new TimeOnly(14, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(12, 0),
                EndTime = new TimeOnly(15, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void WrapAround_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(9, 30),
                EndTime = new TimeOnly(11, 30)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void OneMinute_ShouldReturnFalse()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(0, 0),
                EndTime = new TimeOnly(23, 59)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(23, 59),
                EndTime = new TimeOnly(0, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.False(result);
        }
        [Fact]
        public void AllDay_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(0, 0),
                EndTime = new TimeOnly(23, 59)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
        }
        [Fact]
        public void OneMinuteOverlap_ShouldReturnTrue()
        {
            var existingBooking = new Booking
            {
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 0)
            };
            var newBooking = new Booking
            {
                StartTime = new TimeOnly(10, 59),
                EndTime = new TimeOnly(12, 0)
            };
            bool result = BookingOverlapHelper.IsOverlapping(existingBooking, newBooking);
            Assert.True(result);
    }

    }
}
