// Configuration - Update this if API runs on a different port
const API_BASE_URL = 'https://localhost:7249/api';

let currentChart = null;

document.addEventListener('DOMContentLoaded', () => {
    loadCompanies();
});

async function loadCompanies() {
    const listContainer = document.getElementById('company-list');
    try {
        const response = await fetch(`${API_BASE_URL}/companies`);
        if (!response.ok) {
            throw new Error('Failed to fetch companies');
        }
        const companies = await response.json();

        listContainer.innerHTML = ''; // Clear loading spinner

        if (companies.length === 0) {
            listContainer.innerHTML = '<div class="p-3 text-muted">No companies found.</div>';
            return;
        }

        companies.forEach(company => {
            const btn = document.createElement('button');
            btn.className = 'list-group-item list-group-item-action';
            btn.innerHTML = `<strong>${company.symbol}</strong> <small class="text-muted d-block">${company.name}</small>`;
            btn.onclick = () => selectCompany(company.symbol, btn);
            listContainer.appendChild(btn);
        });

    } catch (error) {
        console.error(error);
        listContainer.innerHTML = '<div class="p-3 text-danger">Error loading companies. Is the API running?</div>';
    }
}

async function selectCompany(symbol, element) {
    // Highlight selected
    document.querySelectorAll('#company-list .list-group-item').forEach(el => el.classList.remove('active'));
    element.classList.add('active');

    // Update title
    document.getElementById('chart-title').innerText = `${symbol} - Last 30 Days Performance`;

    // Load Data
    await loadStockData(symbol);
    await loadSummary(symbol);
}

async function loadStockData(symbol) {
    try {
        const response = await fetch(`${API_BASE_URL}/data/${symbol}`);
        if (!response.ok) {
            alert('Error loading data for ' + symbol);
            return;
        }
        const data = await response.json();

        const labels = data.map(d => new Date(d.date).toLocaleDateString());
        const closePrices = data.map(d => d.close);
        const ma7 = data.map(d => d.movingAverage7);

        renderChart(symbol, labels, closePrices, ma7);

    } catch (error) {
        console.error(error);
        alert('Failed to load chart data');
    }
}

async function loadSummary(symbol) {
    try {
        const response = await fetch(`${API_BASE_URL}/summary/${symbol}`);
        if (response.ok) {
            const data = await response.json();
            document.getElementById('stat-high').innerText = formatCurrency(data.fiftyTwoWeekHigh);
            document.getElementById('stat-low').innerText = formatCurrency(data.fiftyTwoWeekLow);
            document.getElementById('stat-avg').innerText = formatCurrency(data.averageClose);
            document.getElementById('summary-section').style.display = 'flex';
        }
    } catch (error) {
        console.error(error);
    }
}

function renderChart(symbol, labels, closePrices, ma7) {
    const ctx = document.getElementById('stockChart').getContext('2d');

    if (currentChart) {
        currentChart.destroy();
    }

    currentChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Close Price',
                    data: closePrices,
                    borderColor: 'rgb(75, 192, 192)',
                    backgroundColor: 'rgba(75, 192, 192, 0.1)',
                    tension: 0.1,
                    fill: true
                },
                {
                    label: '7-Day MA',
                    data: ma7,
                    borderColor: 'rgb(255, 99, 132)',
                    borderDash: [5, 5],
                    tension: 0.1,
                    fill: false,
                    pointRadius: 0
                }
            ]
        },
        options: {
            responsive: true,
            interaction: {
                mode: 'index',
                intersect: false,
            },
            plugins: {
                title: {
                    display: true,
                    text: `${symbol} Stock Analysis`
                }
            }
        }
    });
}

function formatCurrency(val) {
    return new Intl.NumberFormat('en-IN', { style: 'currency', currency: 'INR' }).format(val);
}
