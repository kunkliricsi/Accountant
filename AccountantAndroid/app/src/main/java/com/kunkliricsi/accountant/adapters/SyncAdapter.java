package com.kunkliricsi.accountant.adapters;

import android.accounts.Account;
import android.content.AbstractThreadedSyncAdapter;
import android.content.ContentProviderClient;
import android.content.ContentResolver;
import android.content.Context;
import android.content.SyncResult;
import android.os.Bundle;
import android.util.Log;

public class SyncAdapter extends AbstractThreadedSyncAdapter {

    private static final String TAG = SyncAdapter.class.getSimpleName();

    SyncAdapter(final Context context, final boolean autoInitialize) {
        super(context, autoInitialize);
    }

    SyncAdapter(final Context context, final boolean autoInitialize,
                             final boolean allowParallelSyncs) {
        super(context, autoInitialize, allowParallelSyncs);
        manager = new ApolloBackendConfigurationManager(context);
    }

    /**
     * Perform a sync for this account. SyncAdapter-specific parameters may be specified in extras,
     * which is guaranteed to not be null. Invocations of this method are guaranteed to be
     * serialized.
     *
     * @param account    the account that should be synced
     * @param extras     SyncAdapter-specific parameters
     * @param authority  the authority of this sync request
     * @param provider   a ContentProviderClient that points to the ContentProvider for this
     *                   authority
     * @param syncResult SyncAdapter-specific parameters
     */
    @Override
    public void onPerformSync(final Account account, final Bundle extras, final String authority,
                              final ContentProviderClient provider, final SyncResult syncResult) {
        Log.i(TAG, "onPerformSync() was called");

        // do networking stuff here
    }
}
}
