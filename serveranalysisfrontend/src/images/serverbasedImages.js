import React from 'react';
import * as serverbasedImages from "./serverbased";

const ServerBasedImages = () => {
  return (
    <div>
      <img src={serverbasedImages.serverbasedIntroduction} alt="Introduction" />
      <img src={serverbasedImages.serverbasedAdvantages} alt="Advantages" />
      <img src={serverbasedImages.serverbasedApproach} alt="Approach" />
      <img src={serverbasedImages.serverbasedUseCases} alt="Use Cases" />
      <img src={serverbasedImages.serverbasedLimitations} alt="Limitations" />
      <img src={serverbasedImages.serverbasedComparison} alt="Comparison" />
      <img src={serverbasedImages.serverbasedIndustryInsights} alt="Industry Insights" />
      <img src={serverbasedImages.serverbasedBeneficiaries} alt="Beneficiaries" />
    </div>
  );
};

export default ServerBasedImages;