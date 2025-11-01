// Initialisation du graphique des projets
window.initProjectChart = (dotNetHelper) => {
    // Récupération du contexte du canvas
    const ctx = document.getElementById('projectChart');
    // Si pas de contexte, on quitte la fonction
    if (!ctx) return;

    // Destruction du graphique existant s'il y en a un
    if (ctx.chart) {
        ctx.chart.destroy();
    }

    // Création d'un nouveau graphique
    ctx.chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [],
            datasets: [{
                label: 'Nombre de projets',
                data: [],
                backgroundColor: ['#009591', '#A1E7D7', '#BED600', '#006D55', '#BED600', '#E98300', '#E4E4E4', '#BED600'],
                borderWidth: 0.5,
                borderRadius: 5
            }]
        },
        options: {
            // Orientation horizontale
            indexAxis: 'y',
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: { beginAtZero: true, grid: { color: '#eee' } },
                y: { grid: { display: false } }
            },
            plugins: { legend: { display: false } },
            // Gestion du clic sur une barre
            onClick: (evt, elements) => {
                // Si aucun élément n'est cliqué, on quitte la fonction
                if (!elements.length) return;

                // Récupération de l'index de l'élément cliqué
                const idx = elements[0].index;
                // Récupération du label correspondant
                const label = ctx.chart.data.labels[idx];
                // Appel de la méthode C# pour naviguer vers les projets
                if (label && dotNetHelper) {
                    dotNetHelper.invokeMethodAsync('NavigateToProjects', label);
                }
            }
        }
    });
};

// Mise à jour des données du graphique des projets
window.updateProjectChart = (labels, values) => {
    // Récupération du contexte du canvas
    const ctx = document.getElementById('projectChart');
    // Si pas de contexte ou de graphique, on quitte la fonction
    if (!ctx || !ctx.chart) return;

    // Mise à jour des labels et des données
    ctx.chart.data.labels = labels;
    ctx.chart.data.datasets[0].data = values;
    ctx.chart.update();
};






