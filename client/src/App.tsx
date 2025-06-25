import { Banner } from './components/Banner';
import digitalCareerUrl from './assets/digital-career.jpg'
import winterOfferUrl from './assets/winter-offer.jpg'

export function App() {
  const externalPageUrl = "https://react.dev/";

  return (
    <div>
      <Banner 
        bannerId="digital-career-vertical" 
        imageUrl={digitalCareerUrl} 
      />

      <iframe 
        src={externalPageUrl}
        title="iFrame"
        style={{ 
          width: '100%', 
          height: '800px',
          border: '1px solid #ccc'
        }}
      >
        <p>Ваш браузер не поддерживает iframes.</p>
      </iframe>

      <Banner 
        bannerId="winter-offer-horizontal" 
        imageUrl={winterOfferUrl}
      />
    </div>
  );
}