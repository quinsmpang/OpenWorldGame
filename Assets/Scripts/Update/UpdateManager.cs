﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SLua;

public class UpdateManager : Singleton<UpdateManager>
{
    public enum UpdateFileStateType
    {
        BeginDownLoad,
        FinishDownload,
        MoveFile,
        Finished
    }

    public enum DownloadFileType
    {
        TypeText,
        TypeAssetBundle,
        None
    }

    public class DownloadFileRequest
    {
        public int Id;
        public string fileUrl;
        public string filePath;
        public DownloadFileType fileType;
        public OnScriptDownloadFinishedEvent onScriptDownloaded;
    }

    public delegate void OnScriptDownloadFinishedEvent(string script);
    public delegate void OnUpdateStateChangedEvent(string filePath, UpdateFileStateType updateState);
	public const string UpdateTest = "/Users/nottvlike/Documents/github/Update";

    public OnUpdateStateChangedEvent OnUpdateStateChanged = null;
    List<DownloadFileRequest> _downloadFileList = new List<DownloadFileRequest>();
    UpdateFileStateType _state = UpdateFileStateType.Finished;
    string _currentUpdateFilePath = "";

    public UpdateFileStateType State
    {
        get 
        {
            return _state;
        }
        set
        {
            _state = value;
            
            if (OnUpdateStateChanged != null)
                OnUpdateStateChanged(System.IO.Path.GetFileName(_currentUpdateFilePath), _state);

            if (value == UpdateFileStateType.Finished)
            {
                startDownloadFile();
            }
        }
    }

    public static UpdateManager getInstance()
    {
        return Instance;
    }

    public void download(string url, string targetPath, DownloadFileType fileType, OnScriptDownloadFinishedEvent downloadedEvent = null)
    {
        var file = new DownloadFileRequest();
        file.Id = System.DateTime.Now.Millisecond;
        file.fileUrl = url;
        file.filePath = targetPath;
        file.fileType = fileType;
        file.onScriptDownloaded = downloadedEvent;
        _downloadFileList.Add(file);

		startDownloadFile();
    }

    void startDownloadFile()
    {
        if (State != UpdateFileStateType.Finished)
            return;

        if (_downloadFileList.Count <= 0)
            return;

        var req = _downloadFileList[0];
        _currentUpdateFilePath = req.filePath;
        State = UpdateFileStateType.BeginDownLoad;
        StartCoroutine(downloadFile(req));
    }

    IEnumerator downloadFile(DownloadFileRequest req)
    {
        WWW www = new WWW(req.fileUrl);
        yield return www;
        State = UpdateFileStateType.FinishDownload;

        State = UpdateFileStateType.MoveFile;
        moveFile(req, www.text, www.bytes, www.bytes.Length);

		_downloadFileList.Remove(req);
        State = UpdateFileStateType.Finished;

		if (req.onScriptDownloaded != null)
		{
			req.onScriptDownloaded(www.text);
		}
    }

    void moveFile(DownloadFileRequest req, string info, byte[] bytes, int length)
    {
        if (req.fileType == DownloadFileType.TypeText)
        {
            FileManager.createFileWithString(req.filePath, info);
        }
        else if (req.fileType == DownloadFileType.TypeAssetBundle)
        {
            FileManager.createFileWithBytes(req.filePath, bytes, length);
        }
    }
}
