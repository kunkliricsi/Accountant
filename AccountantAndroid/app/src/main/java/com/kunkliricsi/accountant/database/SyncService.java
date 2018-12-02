package com.kunkliricsi.accountant.database;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;

import java.io.PushbackReader;

import android.support.annotation.Nullable;

public class SyncService extends Service {

    private static SyncAdapter adapter = null;

    private static final Object adapterLock = new Object();

    public void OnCreate() {
        synchronized (adapterLock) {
            if (adapter == null) {
                adapter = new SyncAdapter(getApplicationContext(), true);
            }
        }
    }

    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return adapter.getSyncAdapterBinder();
    }
}
