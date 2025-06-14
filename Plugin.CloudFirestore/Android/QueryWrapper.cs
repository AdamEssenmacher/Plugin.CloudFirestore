﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Runtime;
using Firebase.Firestore;

namespace Plugin.CloudFirestore
{
    public class QueryWrapper : IQuery, IEquatable<QueryWrapper>
    {
        private readonly Query _query;

        public QueryWrapper(Query query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public IFirebaseFirestore FirebaseFirestore => FirestoreProvider.GetFirestore(_query.Firestore);

        public IQuery LimitTo(long limit)
        {
            var query = _query.Limit(limit);
            return new QueryWrapper(query);
        }

        public IQuery LimitToLast(long limit)
        {
            var query = _query.LimitToLast(limit);
            return new QueryWrapper(query);
        }

        public IQuery OrderBy(string field)
        {
            var query = _query.OrderBy(field);
            return new QueryWrapper(query);
        }

        public IQuery OrderBy(FieldPath field)
        {
            var query = _query.OrderBy(field?.ToNative());
            return new QueryWrapper(query);
        }

        public IQuery OrderBy(string field, bool descending)
        {
            var direction = descending ? Query.Direction.Descending : Query.Direction.Ascending;

            var query = _query.OrderBy(field, direction);
            return new QueryWrapper(query);
        }

        public IQuery OrderBy(FieldPath field, bool descending)
        {
            var direction = descending ? Query.Direction.Descending : Query.Direction.Ascending;

            var query = _query.OrderBy(field?.ToNative(), direction);
            return new QueryWrapper(query);
        }

        public IQuery WhereEqualsTo(string field, object? value)
        {
            var query = _query.WhereEqualTo(field, value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereEqualsTo(FieldPath field, object? value)
        {
            var query = _query.WhereEqualTo(field?.ToNative(), value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereGreaterThan(string field, object value)
        {
            var query = _query.WhereGreaterThan(field, value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereGreaterThan(FieldPath field, object value)
        {
            var query = _query.WhereGreaterThan(field?.ToNative(), value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereGreaterThanOrEqualsTo(string field, object value)
        {
            var query = _query.WhereGreaterThanOrEqualTo(field, value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereGreaterThanOrEqualsTo(FieldPath field, object value)
        {
            var query = _query.WhereGreaterThanOrEqualTo(field?.ToNative(), value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereLessThan(string field, object value)
        {
            var query = _query.WhereLessThan(field, value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereLessThan(FieldPath field, object value)
        {
            var query = _query.WhereLessThan(field?.ToNative(), value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereLessThanOrEqualsTo(string field, object value)
        {
            var query = _query.WhereLessThanOrEqualTo(field, value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereLessThanOrEqualsTo(FieldPath field, object value)
        {
            var query = _query.WhereLessThanOrEqualTo(field?.ToNative(), value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereArrayContains(string field, object value)
        {
            var query = _query.WhereArrayContains(field, value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereArrayContains(FieldPath field, object value)
        {
            var query = _query.WhereArrayContains(field?.ToNative(), value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereArrayContainsAny(string field, IEnumerable<object> values)
        {
            var query = _query.WhereArrayContainsAny(field, values?.Select(x => x.ToNativeFieldValue()).ToList());
            return new QueryWrapper(query);
        }

        public IQuery WhereArrayContainsAny(FieldPath field, IEnumerable<object> values)
        {
            var query = _query.WhereArrayContainsAny(field?.ToNative(), values?.Select(x => x.ToNativeFieldValue()).ToList());
            return new QueryWrapper(query);
        }

        public IQuery WhereIn(string field, IEnumerable<object> values)
        {
            var query = _query.WhereIn(field, values?.Select(x => x.ToNativeFieldValue()).ToList());
            return new QueryWrapper(query);
        }

        public IQuery WhereIn(FieldPath field, IEnumerable<object> values)
        {
            var query = _query.WhereIn(field?.ToNative(), values?.Select(x => x.ToNativeFieldValue()).ToList());
            return new QueryWrapper(query);
        }

        public IQuery WhereNotEqualTo(string field, object value)
        {
            var query = _query.WhereNotEqualTo(field, value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereNotEqualTo(FieldPath field, object value)
        {
            var query = _query.WhereNotEqualTo(field?.ToNative(), value.ToNativeFieldValue());
            return new QueryWrapper(query);
        }

        public IQuery WhereNotIn(string field, IEnumerable<object> values)
        {
            var query = _query.WhereNotIn(field, values?.Select(x => x.ToNativeFieldValue()).ToList());
            return new QueryWrapper(query);
        }

        public IQuery WhereNotIn(FieldPath field, IEnumerable<object> values)
        {
            var query = _query.WhereNotIn(field?.ToNative(), values?.Select(x => x.ToNativeFieldValue()).ToList());
            return new QueryWrapper(query);
        }

        public IQuery StartAt(IDocumentSnapshot document)
        {
            var query = _query.StartAt(document.ToNative());
            return new QueryWrapper(query);
        }

        public IQuery StartAt(params object?[] fieldValues)
        {
            var query = _query.StartAt(fieldValues.Select(x => x.ToNativeFieldValue()).ToArray());
            return new QueryWrapper(query);
        }

        public IQuery StartAfter(IDocumentSnapshot document)
        {
            var query = _query.StartAfter(document.ToNative());
            return new QueryWrapper(query);
        }

        public IQuery StartAfter(params object?[] fieldValues)
        {
            var query = _query.StartAfter(fieldValues.Select(x => x.ToNativeFieldValue()).ToArray());
            return new QueryWrapper(query);
        }

        public IQuery EndAt(IDocumentSnapshot document)
        {
            var query = _query.EndAt(document.ToNative());
            return new QueryWrapper(query);
        }

        public IQuery EndAt(params object?[] fieldValues)
        {
            var query = _query.EndAt(fieldValues.Select(x => x.ToNativeFieldValue()).ToArray());
            return new QueryWrapper(query);
        }

        public IQuery EndBefore(IDocumentSnapshot document)
        {
            var query = _query.EndBefore(document.ToNative());
            return new QueryWrapper(query);
        }

        public IQuery EndBefore(params object?[] fieldValues)
        {
            var query = _query.EndBefore(fieldValues.Select(x => x.ToNativeFieldValue()).ToArray());
            return new QueryWrapper(query);
        }

        public void GetDocuments(QuerySnapshotHandler handler)
        {
            _query.Get().AddOnCompleteListener(new OnCompleteHandlerListener((task) =>
            {
                var snapshot = !task.IsSuccessful ? null : task.Result.JavaCast<QuerySnapshot>();
                handler?.Invoke(snapshot == null ? null : new QuerySnapshotWrapper(snapshot),
                                task.IsSuccessful ? null : ExceptionMapper.Map(task.Exception));
            }));
        }

        public void GetDocuments(Source source, QuerySnapshotHandler handler)
        {
            _query.Get(source.ToNative()).AddOnCompleteListener(new OnCompleteHandlerListener((task) =>
            {
                var snapshot = !task.IsSuccessful ? null : task.Result.JavaCast<QuerySnapshot>();
                handler?.Invoke(snapshot == null ? null : new QuerySnapshotWrapper(snapshot),
                                task.IsSuccessful ? null : ExceptionMapper.Map(task.Exception));
            }));
        }

        public Task<IQuerySnapshot> GetDocumentsAsync()
        {
            return GetAsync();
        }

        public Task<IQuerySnapshot> GetAsync()
        {
            var tcs = new TaskCompletionSource<IQuerySnapshot>();

            _query.Get().AddOnCompleteListener(new OnCompleteHandlerListener((task) =>
            {
                if (task.IsSuccessful)
                {
                    var snapshot = task.Result.JavaCast<QuerySnapshot>();
                    tcs.SetResult(new QuerySnapshotWrapper(snapshot!));
                }
                else
                {
                    tcs.SetException(ExceptionMapper.Map(task.Exception));
                }
            }));

            return tcs.Task;
        }

        public Task<IQuerySnapshot> GetDocumentsAsync(Source source)
        {
            return GetAsync(source);
        }

        public Task<IQuerySnapshot> GetAsync(Source source)
        {
            var tcs = new TaskCompletionSource<IQuerySnapshot>();

            _query.Get(source.ToNative()).AddOnCompleteListener(new OnCompleteHandlerListener((task) =>
            {
                if (task.IsSuccessful)
                {
                    var snapshot = task.Result.JavaCast<QuerySnapshot>();
                    tcs.SetResult(new QuerySnapshotWrapper(snapshot!));
                }
                else
                {
                    tcs.SetException(ExceptionMapper.Map(task.Exception));
                }
            }));

            return tcs.Task;
        }

        public IListenerRegistration AddSnapshotListener(QuerySnapshotHandler listener)
        {
            var registration = _query.AddSnapshotListener(new EventHandlerListener<QuerySnapshot>((value, error) =>
            {
                listener?.Invoke(value == null ? null : new QuerySnapshotWrapper(value),
                                 error == null ? null : ExceptionMapper.Map(error));
            }));

            return new ListenerRegistrationWrapper(registration);
        }

        public IListenerRegistration AddSnapshotListener(bool includeMetadataChanges, QuerySnapshotHandler listener)
        {
            var registration = _query.AddSnapshotListener(includeMetadataChanges ? MetadataChanges.Include : MetadataChanges.Exclude, new EventHandlerListener<QuerySnapshot>((value, error) =>
           {
               listener?.Invoke(value == null ? null : new QuerySnapshotWrapper(value),
                                error == null ? null : ExceptionMapper.Map(error));
           }));

            return new ListenerRegistrationWrapper(registration);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as QueryWrapper);
        }

        public bool Equals(QueryWrapper? other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            if (ReferenceEquals(_query, other._query)) return true;
            return _query.Equals(other._query);
        }

        public override int GetHashCode()
        {
            return _query?.GetHashCode() ?? 0;
        }
    }
}
