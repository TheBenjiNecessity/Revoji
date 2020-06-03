using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables;

namespace RevojiWebTests
{
    [TestClass]
    public class DBReviewTests
    {
        string fullReviewJson = @"{
            app_user_id: 1,
            reviewable_id: 1,
            title: 'test',
            comment: 'test comment',
            emojis: 'LikeIt',
            reviewable: {
                title: 'Inception',
                type: 'media',
                tp_id: 't12345678',
                tp_name: 'imdb',
                description: 'A movie about sleeping',
                title_image_url: 'http://www.test.com',
                content: {
                    avatar: 'avatar'
                },
                info: {
                    test: 'this is a test'
                }
            },
            app_user: {
                first_name: 'Ben',
                last_name: 'Wishart',
                gender: 'Male',
                religion: 'Catholic',
                politics: 'Republican',
                education: 'Bachelors',
                profession: 'Engineer',
                interests: 'some,interests',
                city: 'Bedford',
                administrative_area: 'Nova Scotia',
                country: 'CA',
                dob: '2011-10-05',
                handle: 'testHandle',
                email: 'test@test.com',
                password: 'testPassword',
                content: {
                    avatar: 'avatar'
                },
                settings: {
                    test: 'this is a test'
                },
                preferences: {
                    test: 'this is a test'
                }
            }
        }";

        string partialReviewJson = @"{
            app_user_id: 1,
            reviewable_id: 1,
            comment: 'test comment',
            emojis: 'LikeIt',
            reviewable: {
                title: 'Inception',
                type: 'media',
                tp_id: 't12345678',
                tp_name: 'imdb',
                description: 'A movie about sleeping',
                title_image_url: 'http://www.test.com'
            },
            app_user: {
                first_name: 'Ben',
                last_name: 'Wishart',
                gender: 'Male',
                religion: 'Catholic',
                politics: 'Republican',
                education: 'Bachelors',
                profession: 'Engineer',
                interests: 'some,interests',
                city: 'Bedford',
                administrative_area: 'Nova Scotia',
                country: 'CA',
                dob: '2011-10-05',
                handle: 'testHandle',
                email: 'test@test.com',
                password: 'testPassword'
            }
        }";

        [TestMethod]
        public void FullReviewSuccessTest()
        {
            JObject fullReviewJObject = JObject.Parse(fullReviewJson);
            DBReview fullDBReview = new DBReview(fullReviewJObject);

            Assert.AreEqual(fullDBReview.AppUserId, 1);
            Assert.AreEqual(fullDBReview.ReviewableId, 1);
            Assert.AreEqual(fullDBReview.Title, "test");
            Assert.AreEqual(fullDBReview.Comment, "test comment");
            Assert.AreEqual(fullDBReview.Emojis, "LikeIt");

            Assert.AreEqual(fullDBReview.DBReviewable.Title, "Inception");
            Assert.AreEqual(fullDBReview.DBReviewable.Type, "media");
            Assert.AreEqual(fullDBReview.DBReviewable.TpId, "t12345678");
            Assert.AreEqual(fullDBReview.DBReviewable.TpName, "imdb");
            Assert.AreEqual(fullDBReview.DBReviewable.Description, "A movie about sleeping");
            Assert.AreEqual(fullDBReview.DBReviewable.TitleImageUrl, "http://www.test.com");
            Assert.AreEqual(fullDBReview.DBReviewable.Content, "{\"avatar\":\"avatar\"}");
            Assert.AreEqual(fullDBReview.DBReviewable.Info, "{\"test\":\"this is a test\"}");

            Assert.AreEqual(fullDBReview.DBAppUser.FirstName, "Ben");
            Assert.AreEqual(fullDBReview.DBAppUser.LastName, "Wishart");
            Assert.AreEqual(fullDBReview.DBAppUser.Gender, "Male");
            Assert.AreEqual(fullDBReview.DBAppUser.Religion, "Catholic");
            Assert.AreEqual(fullDBReview.DBAppUser.Politics, "Republican");
            Assert.AreEqual(fullDBReview.DBAppUser.Education, "Bachelors");
            Assert.AreEqual(fullDBReview.DBAppUser.Profession, "Engineer");
            Assert.AreEqual(fullDBReview.DBAppUser.Interests, "some,interests");
            Assert.AreEqual(fullDBReview.DBAppUser.City, "Bedford");
            Assert.AreEqual(fullDBReview.DBAppUser.AdministrativeArea, "Nova Scotia");
            Assert.AreEqual(fullDBReview.DBAppUser.Country, "CA");
            Assert.AreEqual(fullDBReview.DBAppUser.Handle, "testHandle");
            Assert.AreEqual(fullDBReview.DBAppUser.Email, "test@test.com");
            Assert.AreEqual(fullDBReview.DBAppUser.Content, "{\"avatar\":\"avatar\"}");
            Assert.AreEqual(fullDBReview.DBAppUser.Settings, "{\"test\":\"this is a test\"}");
            Assert.AreEqual(fullDBReview.DBAppUser.Preferences, "{\"test\":\"this is a test\"}");

            Assert.IsNotNull(fullDBReview.DBAppUser.DateOfBirth);
            Assert.AreEqual(fullDBReview.DBAppUser.DateOfBirth.Value.Year, 2011);
            Assert.AreEqual(fullDBReview.DBAppUser.DateOfBirth.Value.Month, 10);
            Assert.AreEqual(fullDBReview.DBAppUser.DateOfBirth.Value.Day, 5);
        }

        [TestMethod]
        public void PartialReviewSuccessTest()
        {
            JObject fullReviewJObject = JObject.Parse(fullReviewJson);
            DBReview fullDBReview = new DBReview(fullReviewJObject);

            Assert.AreEqual(fullDBReview.AppUserId, 1);
            Assert.AreEqual(fullDBReview.ReviewableId, 1);
            Assert.AreEqual(fullDBReview.Title, null);
            Assert.AreEqual(fullDBReview.Comment, "test comment");
            Assert.AreEqual(fullDBReview.Emojis, "LikeIt");

            Assert.AreEqual(fullDBReview.DBReviewable.Title, "Inception");
            Assert.AreEqual(fullDBReview.DBReviewable.Type, "media");
            Assert.AreEqual(fullDBReview.DBReviewable.TpId, "t12345678");
            Assert.AreEqual(fullDBReview.DBReviewable.TpName, "imdb");
            Assert.AreEqual(fullDBReview.DBReviewable.Description, "A movie about sleeping");
            Assert.AreEqual(fullDBReview.DBReviewable.TitleImageUrl, "http://www.test.com");
            Assert.AreEqual(fullDBReview.DBReviewable.Content, null);
            Assert.AreEqual(fullDBReview.DBReviewable.Info, null);

            Assert.AreEqual(fullDBReview.DBAppUser.FirstName, "Ben");
            Assert.AreEqual(fullDBReview.DBAppUser.LastName, "Wishart");
            Assert.AreEqual(fullDBReview.DBAppUser.Gender, "Male");
            Assert.AreEqual(fullDBReview.DBAppUser.Religion, "Catholic");
            Assert.AreEqual(fullDBReview.DBAppUser.Politics, "Republican");
            Assert.AreEqual(fullDBReview.DBAppUser.Education, "Bachelors");
            Assert.AreEqual(fullDBReview.DBAppUser.Profession, "Engineer");
            Assert.AreEqual(fullDBReview.DBAppUser.Interests, "some,interests");
            Assert.AreEqual(fullDBReview.DBAppUser.City, "Bedford");
            Assert.AreEqual(fullDBReview.DBAppUser.AdministrativeArea, "Nova Scotia");
            Assert.AreEqual(fullDBReview.DBAppUser.Country, "CA");
            Assert.AreEqual(fullDBReview.DBAppUser.Handle, "testHandle");
            Assert.AreEqual(fullDBReview.DBAppUser.Email, "test@test.com");
            Assert.AreEqual(fullDBReview.DBAppUser.Content, null);
            Assert.AreEqual(fullDBReview.DBAppUser.Settings, null);
            Assert.AreEqual(fullDBReview.DBAppUser.Preferences, null);

            Assert.IsNotNull(fullDBReview.DBAppUser.DateOfBirth);
            Assert.AreEqual(fullDBReview.DBAppUser.DateOfBirth.Value.Year, 2011);
            Assert.AreEqual(fullDBReview.DBAppUser.DateOfBirth.Value.Month, 10);
            Assert.AreEqual(fullDBReview.DBAppUser.DateOfBirth.Value.Day, 5);
        }
    }
}
