package com.kunkliricsi.accountant;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.BottomNavigationView;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import com.kunkliricsi.accountant.fragments.AddExpenseDialogFragment;
import com.kunkliricsi.accountant.fragments.ReportsFragment;
import com.kunkliricsi.accountant.fragments.ShoppingListFragment;

public class MainActivity extends AppCompatActivity {

    private com.github.clans.fab.FloatingActionButton fab_expense;
    private TextView mTextMessage;

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

        mTextMessage = (TextView) findViewById(R.id.message);
        BottomNavigationView navigation = (BottomNavigationView) findViewById(R.id.navigation);
        navigation.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener);
        navigation.setSelectedItemId(R.id.navigation_shopping_list);

        fab_expense = findViewById(R.id.fab_expense);
        fab_expense.setOnClickListener(onFab_ExpenseClickListener);

    }
}
