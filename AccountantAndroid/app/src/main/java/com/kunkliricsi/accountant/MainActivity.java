package com.kunkliricsi.accountant;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.app.Activity;
import android.content.ContentResolver;
import android.os.Bundle;
import android.os.StrictMode;
import android.support.annotation.NonNull;
import android.support.design.widget.BaseTransientBottomBar;
import android.support.design.widget.BottomNavigationView;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.TextView;

import com.kunkliricsi.accountant.database.DatabaseApi;
import com.kunkliricsi.accountant.database.authenticator.Authenticator;
import com.kunkliricsi.accountant.database.local.entities.User;
import com.kunkliricsi.accountant.fragments.AddExpenseDialogFragment;
import com.kunkliricsi.accountant.fragments.AddShoppingListItemDialogFragment;
import com.kunkliricsi.accountant.fragments.ReportsFragment;
import com.kunkliricsi.accountant.fragments.ShoppingListFragment;

public class MainActivity extends AppCompatActivity {

    private com.github.clans.fab.FloatingActionMenu fab_menu;
    private com.github.clans.fab.FloatingActionButton fab_expense;
    private com.github.clans.fab.FloatingActionButton fab_shopping;
    private TextView mTextMessage;

    private DatabaseApi databaseApi;

    public static final long SECONDS_PER_MINUTE = 60L;
    public static final long SYNC_INTERVAL_IN_MINUTES = 60L;
    public static final long SYNC_INTERVAL =
            SYNC_INTERVAL_IN_MINUTES *
                    SECONDS_PER_MINUTE;

    private BottomNavigationView.OnNavigationItemSelectedListener mOnNavigationItemSelectedListener
            = new BottomNavigationView.OnNavigationItemSelectedListener() {

        @Override
        public boolean onNavigationItemSelected(@NonNull MenuItem item) {
            Fragment selectedFragment = null;
            switch (item.getItemId()) {
                case R.id.navigation_shopping_list:
                    selectedFragment = ShoppingListFragment.newInstance();
                    break;
                case R.id.navigation_reports:
                    selectedFragment = ReportsFragment.newInstance();
                    break;
                case R.id.navigation_statistics:
                    selectedFragment = ReportsFragment.newInstance();
                    break;
            }
            FragmentTransaction transaction = getSupportFragmentManager().beginTransaction();
            transaction.replace(R.id.fragment_container, selectedFragment);
            transaction.commit();
            return true;
        }
    };

    private FloatingActionButton.OnClickListener onFab_ExpenseClickListener
            = new FloatingActionButton.OnClickListener() {

        @Override
        public void onClick(View v) {
            new AddExpenseDialogFragment().show(getSupportFragmentManager(), "EXPENSE_TAG");
            fab_menu.close(false);
        }
    };

    private com.github.clans.fab.FloatingActionButton.OnClickListener onFab_ShoppingClickListener = new View.OnClickListener() {
        @Override
        public void onClick(View v) {
            new AddShoppingListItemDialogFragment().show(getSupportFragmentManager(), "SHOPPING_TAG");
            fab_menu.close(false);
        }
    };

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.settings, menu);

        return true;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        AccountManager manager = AccountManager.get(this);
        final Account account = new Account(getPackageName(), Authenticator.ACCOUNT);
        if (manager.addAccountExplicitly(account, null, null)) {
            ContentResolver.setIsSyncable(account, Authenticator.AUTHORITY, 1);
            ContentResolver.setSyncAutomatically(account, Authenticator.AUTHORITY, true);
            ContentResolver.addPeriodicSync(account, Authenticator.AUTHORITY, Bundle.EMPTY, SYNC_INTERVAL);
        }
        try {
            DatabaseApi.Initialize(getApplicationContext(),null);
        } catch (Exception ex){
            Snackbar.make(findViewById(R.id.fragment_container), ex.getMessage(), 5000).show();
        }

        mTextMessage = (TextView) findViewById(R.id.message);
        BottomNavigationView navigation = (BottomNavigationView) findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);
        navigation.setSelectedItemId(R.id.navigation_shopping_list);

        fab_menu = findViewById(R.id.fab_menu);
        fab_expense = findViewById(R.id.fab_expense);
        fab_expense.setOnClickListener(onFab_ExpenseClickListener);
        fab_shopping = findViewById(R.id.fab_shopping);
        fab_shopping.setOnClickListener(onFab_ShoppingClickListener);
    }
}
