import { useRef, useEffect, useCallback } from 'react';
import { sendClickEvent, sendViewEvent } from '../services/api';

type Props = {
    bannerId: string;
    imageUrl: string;
};

export function Banner({ bannerId, imageUrl }: Props) {
    const bannerRef = useRef<HTMLImageElement>(null);
    const isIntersectingRef = useRef(false);

    const getOrSetUserId = () => {
        let userId = localStorage.getItem('noruBannerUserId');
        if (!userId) {
            userId = 'user_' + Date.now() + '_' + Math.random().toString(36).substring(2, 9);
            localStorage.setItem('noruBannerUserId', userId);
        }
        return userId;
    };

    const handleView = useCallback(async () => {
        // Вкладка должна быть активна
        if (!isIntersectingRef.current || document.visibilityState !== 'visible') {
            return;
        }

        try {
            const event = { bannerId, userId: getOrSetUserId() };
            await sendViewEvent(event);
            console.log(`View of ${bannerId} sent`);
            document.removeEventListener('visibilitychange', handleView);
        } catch (error: any) {
            console.error(error);
        }
    }, [bannerId]);

    const handleClick = async () => {
        try {
            const event = { bannerId, userId: getOrSetUserId() };
            await sendClickEvent(event);
            console.log(`Click of ${bannerId} sent`);
        } catch (error: any) {
            console.error(error);
        }
    };

    useEffect(() => {
        const bannerElement = bannerRef.current;
        if (!bannerElement) return;

        // Подписываемся на событие изменения состояния вкладки
        document.addEventListener('visibilitychange', handleView);

        const observer = new IntersectionObserver(
            (entries) => {
                const entry = entries[0];

                if (entry.isIntersecting) {
                    isIntersectingRef.current = true;
                    handleView();
                    observer.unobserve(bannerElement);
                }
            },
            { threshold: 1.0 } // 100% баннера должно быть видно для события View
        );

        bannerElement.addEventListener('click', handleClick);
        observer.observe(bannerElement);

        return () => {
            bannerElement.removeEventListener('click', handleClick);
            document.removeEventListener('visibilitychange', handleView);
            observer.disconnect();
        };
    }, [bannerId, handleView]);

    return (
        <img
            ref={bannerRef}
            id={bannerId}
            src={imageUrl}
            alt={`NoruBanner Ad - ${bannerId}`}
            style={{ cursor: 'pointer', display: 'block', margin: '10px', maxWidth: '100%' }}
        />
    );
}