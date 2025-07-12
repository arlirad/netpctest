import { createApp } from 'vue'
import './style.css'
import get from 'axios'
import App from './App.vue'
import { createI18n } from 'vue-i18n'

import './assets/styles/global.css'

let apiRoot = 'http://localhost:5288/api';

const i18n = createI18n({
    locale: 'pl',
    fallbackLocale: 'en',
    messages: {},
});

const loadAllLocales = async () => {
    const {data: locales} = await get(apiRoot + "/localisation");

    for (const locale of locales) {
        const {data: keystrings} = await get(apiRoot + "/localisation/" + locale);
        i18n.global.setLocaleMessage(locale, keystrings);
    }
};

const app = createApp(App);

app.config.globalProperties.$api = apiRoot;

loadAllLocales().then(() => {
    app.use(i18n);
    app.mount('#app');
});
